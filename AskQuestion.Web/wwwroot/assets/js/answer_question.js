const questions = {};
let totalQuestions;
let answeredQuestions = 0;
let correctAnswers = 0;
let wrongAnswers = 0;
function QuestionClick(questionId, choice, correctAnswer) {

    if (questions[questionId] === -1) {

        questions[questionId] = choice;
        answeredQuestions++;
        if (choice == correctAnswer)
            correctAnswers++;
        else
            wrongAnswers++;
        if (answeredQuestions == totalQuestions) {

            setTimeout(function () {
                const data = {
                    NameSurname: $('#name-surname').val().trim()
                }

                const keys = Object.keys(questions);
                for (let i = 0; i < keys.length; i++) {
                    data[`Questions[${i}]`] = keys[i];
                    data[`Choices[${i}]`] = questions[keys[i]];
                }

                $.ajax({
                    url: document.location.href,
                    method: "POST",
                    data: data,
                    error: function (ex, message, mes) {
                        alert(mes);
                    },
                    success: function (data) {
                        if (data.message) {
                            $('#error').text(data.message);
                        } else {
                            $('.card-header').text("Results");
                            $('#answer').html("");
                            $('#main-slider').carousel('next');
                            $('#correct').text(`${correctAnswers} correct!`);
                            $('#wrong').text(`${wrongAnswers} wrong!`);
                        }
                    }
                });
            }, 2500);

        }
        return {
            changed: true,
            correct: choice == correctAnswer,
            correctChoice: correctAnswer,
            next: answeredQuestions != totalQuestions ? 3000 : null
        }

    }

    return {
        changed: false
    }

}

$(document).ready(function () {
    totalQuestions = $("div[id^=question-]").each(function (index) {
        if (index != 0)
            questions[($(this).attr('id').split('-')[1])] = -1;
    }).length - 1;
    $('#question-slider .carousel-control-prev').css("display", "none");
    $('#question-slider .carousel-control-next').css("display", "none");
    $('#question-slider .carousel-indicators').children().removeAttr("data-target").removeAttr("data-slide-to");
    const header = $('.card-header');

    $('#questions-slider .carousel-item').first().addClass("active");
    $('#questions-slider .carousel-indicators').children().first().addClass("active");
    header.text("Asks");

    $('#name-surname').on('input', function () {
        $('#btn-next').attr('disabled', $(this).val().trim().length > 0 ? false : true);
    });

    $('#btn-next').click(function () {
        if ($('#name-surname').val().length > 0) {

            header.text("Please answer all questions.");
            $('#main-slider').carousel('next');
            QuestionInit();

        }
    });

    $('#btn-start').click(function () {
        header.text("Please write your name and surname.");
        $('#questions').html("");
        $('#main-slider').carousel('next');
    });

});