const selectedQuestions = [];
const selectedChoices = [];
function QuestionClick(questionId, choice) {
    const index = selectedQuestions.indexOf(questionId);

    if (index !== -1) {

        if (selectedChoices[index] === choice) {

            selectedChoices.splice(index, 1);
            selectedQuestions.splice(index, 1);
            UpdateUI();
            return {
                clear: true
            }

        } else {

            selectedChoices[index] = choice;
            UpdateUI();
            return {
                changed: true,
                correct: true
            }

        }

    } else {

        const lenght = selectedQuestions.length;
        if ((document.isSigned === true && lenght >= 10) || (document.isSigned === false && lenght >= 4)) {

            return {
                changed: false
            }

        }
        selectedQuestions.push(questionId);
        selectedChoices.push(choice);
        UpdateUI();
        return {
            changed: true,
            correct: true
        }

    }

}

function UpdateUI() {
    let text = selectedQuestions.join(',');
    if (!text)
        text = "NONE";
    $('#selected-questions').text(text);
    $('#btn-ask').attr('disabled', !(selectedQuestions.length > 0));
}

$(document).ready(function () {

    const header = $('.card-header');
    if ($('#card').attr('data-signed') == 1)
        this.isSigned = true;
    else
        this.isSigned = false;

    const childs = $('#main-slider .carousel-inner').children();

    if (this.isSigned === false) {

        childs.eq(0).addClass("active");
        header.text("Please write your name and surname");

    } else {

        childs.eq(1).addClass("active");

        if (document.isSigned === false)
            header.text("Please select up to 4 questions with answers.");
        else
            header.text("Please select up to 10 questions with answers.");

        QuestionInit();

    }

    UpdateUI();

    $('#btn-ask').click(function () {

        if (selectedQuestions.length > 0) {

            const data = {
                NameSurname: $('#name-surname').val().trim()
            }

            for (let i = 0; i < selectedQuestions.length; i++) {
                data[`Questions[${i}]`] = selectedQuestions[i];
                data[`Choices[${i}]`] = selectedChoices[i];
            }

            $.ajax({
                url: "AskQuestion",
                method: "POST",
                data: data,
                error: function (ex, message, mes) {
                    alert(mes);
                },
                success: function (data) {
                    if (data.url) {
                        window.location.href = data.url;
                    } else {
                        $('#error').text(data.message);
                    }

                }
            });

        }

    });

    $('#name-surname').on('input', function () {
        $('#btn-next').attr('disabled', $(this).val().trim().length > 0 ? false : true);
    });

    $('#btn-next').click(function () {
        if ($('#name-surname').val().length > 0) {

            if (document.isSigned === false)
                header.text("Please select up to 4 questions with answers.");
            else
                header.text("Please select up to 10 questions with answers.");

            $('#main-slider').carousel('next');
            QuestionInit();

        }
    });

});