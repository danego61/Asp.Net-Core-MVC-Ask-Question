function QuestionInit() {

    $('#question-slider .carousel-indicators').children().removeClass("active").eq(0).addClass("active");
    $('#question-slider .carousel-inner').children().eq(0).addClass("active");
    $('#question-slider .carousel-item .list-group a').click(function () {

        const questionId = this.getAttribute("data-question");
        const choice = this.getAttribute("data-choice");
        const result = QuestionClick(questionId, choice, $(`#question-${questionId}`).attr('data-answer'));
        if (result.changed === true || result.clear === true) {

            const list = $(`#question-${questionId} .list-group`);
            list.children().removeClass('correct').removeClass('wrong');

            if (result.changed === true) {

                const th = $(this);
                if (result.correct === true) {

                    th.addClass('correct');

                } else {

                    th.addClass('wrong');
                    list.children().eq(result.correctChoice).addClass('correct');

                }
                if (result.next) {
                    setTimeout(function () {
                        $('#question-slider').carousel('next');
                    }, result.next);
                }

            }

        }

    });

}