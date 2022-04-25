using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow25.Data
{
    public class QuestionsRepository
    {
        private string _connString;

        public QuestionsRepository(string connString)
        {
            _connString = connString;
        }

        public List<Question> GetAllQuestions()
        {
            using var context = new QuestionsDataContext(_connString);
            return context.Questions.Include(q => q.QuestionsTags).ThenInclude(qt => qt.Tag).Include(q => q.Likes).Include(q => q.Answers).OrderByDescending(q => q.DatePosted).ToList();
        }

        public void AddUser(string name, string email, string password)
        {
            using var context = new QuestionsDataContext(_connString);
            context.Users.Add(new User
            {
                Name = name,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            });
            context.SaveChanges();
        }

        public User LogIn(string email, string password)
        {
            using var context = new QuestionsDataContext(_connString);
            User user = context.Users.FirstOrDefault(u => u.Email == email);
            if(user == null)
            {
                return null;
            }

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? user : null;
        }
       
        public int GetUserIdByEmail(string email)
        {
            using var context = new QuestionsDataContext(_connString);
            return context.Users.FirstOrDefault(u => u.Email == email).Id;
        }

        public int GetTagIdByName(string name)
        {
            using var context = new QuestionsDataContext(_connString);
            Tag tag = context.Tags.FirstOrDefault(t => t.Name == name);
            return tag != null ? tag.Id : 0;
        }

        public int AddTag(string name)
        {
            using var context = new QuestionsDataContext(_connString);
            Tag tag = new Tag
            {
                Name = name
            };
            context.Tags.Add(tag);
            context.SaveChanges();
            return tag.Id;
        }

        public void AddQuestion(Question question, List<string> tags)
        {
            using var context = new QuestionsDataContext(_connString);
            question.DatePosted = DateTime.Now;
            context.Questions.Add(question);
            context.SaveChanges();

            foreach (string tag in tags)
            {
                int tagId = GetTagIdByName(tag);
                if (tagId == 0)
                {
                    tagId = AddTag(tag);
                }
                context.QuestionsTags.Add(new QuestionsTags
                {
                    QuestionId = question.Id,
                    TagId = tagId
                });                
            }
            context.SaveChanges();
        }

        public Question GetQuestionById(int id)
        {
            using var context = new QuestionsDataContext(_connString);
            return context.Questions.Include(q => q.QuestionsTags).ThenInclude(qt => qt.Tag).Include(q => q.Likes).Include(q => q.Answers).Include(q => q.User).FirstOrDefault(q => q.Id == id);
        }

        public void LikeQuestion(int questionId, int userId)
        {
            using var context = new QuestionsDataContext(_connString);
            context.Likes.Add(new Like 
            { 
                QuestionId = questionId,
                UserId = userId
            });
            context.SaveChanges();
        }

        public int GetLikes(int questionId)
        {
            using var context = new QuestionsDataContext(_connString);
            Question question = context.Questions.Include(q => q.Likes).FirstOrDefault(q => q.Id == questionId);
            return question != null ? question.Likes.Count : 0;
        }

        public bool AlreadyLiked(int questionId, int userId)
        {
            using var context = new QuestionsDataContext(_connString);
            Like like = context.Likes.FirstOrDefault(l => l.QuestionId == questionId && l.UserId == userId);
            return like != null;
        }

        public void AddAnswer(Answer answer)
        {
            using var context = new QuestionsDataContext(_connString);
            answer.DatePosted = DateTime.Now;
            context.Answers.Add(answer);
            context.SaveChanges();
        }
    }
}
