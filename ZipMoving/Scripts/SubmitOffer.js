function OpenModal()
{
    var send = { };
    $.get("ReturnTotalCost", send, function (data) {
        $("#TotalCost").html(data);
        $("#totalCostModal").modal("show");
    });
}

//validate email
var validMail = false;
//setup before functions
var typingTimer3;                //timer identifier
var doneTypingInterval3 = 1000;  //time in ms, 5 second for example
var $input3 = $('#email');

//on keyup, start the countdown
$input.on('keyup', function () {
    clearTimeout(typingTimer3);
    typingTimer3 = setTimeout(doneTyping3, doneTypingInterval3);
});

//on keydown, clear the countdown 
$input.on('keydown', function () {
    clearTimeout(typingTimer3);
});

//user is "finished typing," do something
function doneTyping3() {
    var send = { code: $input3.val() };
    $.get("ValidateEmail", send, function (data) {
        validMail = data;
    });
}

function SubmitOffer()
{
    if (validMail) {
        $("#SubmitAll").trigger("click");
        $("#totalCostModal").modal("hide");
    }
    else
    {
        $("#emailText").html("Enter valid email or phone number.");
    }
}

function Cancel()
{
    $("#totalCostModal").modal("hide");
}

