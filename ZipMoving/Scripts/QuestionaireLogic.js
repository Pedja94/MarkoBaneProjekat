//ZipInputCheck
//setup before functions
var typingTimer;                //timer identifier
var doneTypingInterval = 1000;  //time in ms, 5 second for example
var $input = $('#zipINPUT');

//on keyup, start the countdown
$input.on('keyup', function () {
    clearTimeout(typingTimer);
    typingTimer = setTimeout(doneTyping, doneTypingInterval);
});

//on keydown, clear the countdown 
$input.on('keydown', function () {
    clearTimeout(typingTimer);
});

//user is "finished typing," do something
function doneTyping() {
    var send = { code: $input.val() };
    $.get("CheckPickupZipCode", send, function (data) {
        ReturnResult(data);
    });
}

function ReturnResult(data)
{
    if (data == false)
        $("#zipInputText").html('Can\'t move from here. <a href="/index/index#contact">Contact us for more information.</a>');
    else
        $("#zipInputText").html('We can move from here.');
    $("#zipInputText").show();
}

//ZipInputCheck1
//setup before functions
var typingTimer1;                //timer identifier
var doneTypingInterval1 = 1000;  //time in ms, 5 second for example
var $input1 = $('#zipINPUT1');

//on keyup, start the countdown
$input1.on('keyup', function () {
    clearTimeout(typingTimer1);
    typingTimer1 = setTimeout(doneTyping1, doneTypingInterval1);
});

//on keydown, clear the countdown 
$input.on('keydown', function () {
    clearTimeout(typingTimer1);
});

//user is "finished typing," do something
function doneTyping1() {
    var send = { codeFrom: $input.val(), codeTo: $input1.val() }
    $.get("CheckDeliveryZipCode", send, function (data) {
        ReturnResult1(data);
    });
}

function ReturnResult1(data) {
    if (data == false)
        $("#zipInputText1").html('Can\'t move here. <a style="color:white" href="/index/index#contact">Contact us for more information.</a>');

    else
        $("#zipInputText1").html('We can move here.');
    $("#zipInputText1").show();
}

//hiddenPickup
$("#House").click(function () {
    $("#inventory").show();
    $("#HowFullIsStorage").hide();
    $("#SizeOfStorage").hide();
    $("#COI").hide();
    $("#Elevator").hide();
});

$("#Townhouse").click(function () {
    $("#inventory").show();
    $("#HowFullIsStorage").hide();
    $("#SizeOfStorage").hide();
    $("#COI").hide();
    $("#Elevator").hide();
});

$("#Apartment").click(function () {
    $("#inventory").show();
    $("#HowFullIsStorage").hide();
    $("#SizeOfStorage").hide();
    $("#COI").show();
    $("#Elevator").show();
});

$("#Office").click(function () {
    $("#inventory").show();
    $("#HowFullIsStorage").hide();
    $("#SizeOfStorage").hide();
    $("#COI").hide();
    $("#Elevator").show();
});

$("#Storage").click(function () {
    $("#inventory").hide();
    $("#HowFullIsStorage").show();
    $("#SizeOfStorage").show();
    $("#COI").hide();
    $("#Elevator").show();
});

//hiddenDelivery
$("#House1").click(function () {
    $("#COI1").hide();
    $("#Elevator1").hide();
});

$("#Townhouse1").click(function () {
    $("#COI1").hide();
    $("#Elevator1").hide();
});

$("#Apartment1").click(function () {
    $("#COI1").show();
    $("#Elevator1").show();
});

$("#Office1").click(function () {
    $("#COI1").hide();
    $("#Elevator1").show();
});

$("#Storage1").click(function () {
    $("#COI1").hide();
    $("#Elevator1").show();
});