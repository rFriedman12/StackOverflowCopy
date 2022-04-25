$(() => {
    $('#like-button').on('click', function () {
        const questionId = $('#question-id').val();
        $.post('/home/likequestion', { questionId }, function () {
            updateLikes(questionId);
            $('#like-button').remove();
        });
    });

    function updateLikes(questionId) {
        return $.get('/home/getlikes', { questionId }, function (likesCount) {
            $('#likes-count').text(likesCount);
        });
    }
})