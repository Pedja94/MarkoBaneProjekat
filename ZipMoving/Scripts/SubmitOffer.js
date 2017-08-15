var myObject = new Object();
myObject.AdditionalStopOffAtPickup = "";
myObject.ElevatorPickup = "";
myObject.StairsPickup = "";
myObject.ParkingPickup = "";
myObject.HowFullIsStoragePickup = "";
myObject.AdditionalStopOffAtDelivery = "";
myObject.ElevatorDelivery = "";
myObject.StairsDelivery = "";
myObject.ParkingDelivery = "";
myObject.FullPackingService = false;
myObject.StorageService = false;

$("#AdditionalStopPickup").click(function () {
    myObject.AdditionalStopOffAtPickup = $('input[name=AdditionalStopOffAtPickup]:checked').val(); 
});
$("#ElevatorP").click(function () {
    myObject.ElevatorPickup = $('input[name=ElevatorPickup]:checked').val();
});
$("#StairsP").click(function () {
    myObject.StairsPickup = $('input[name=StairsPickup]:checked').val();
});
$("#ParkingP").click(function () {
    myObject.ParkingPickup = $('input[name=ParkingPickup]:checked').val();
});
$("#AdditionalStopDelivery").click(function () {
    myObject.AdditionalStopOffAtDelivery = $('input[name=AdditionalStopOffAtDelivery]:checked').val();
});
$("#ElevatorD").click(function () {
    myObject.ElevatorDelivery = $('input[name=ElevatorDelivery]:checked').val();
});
$("#StairsD").click(function () {
    myObject.StairsDelivery = $('input[name=StairsDelivery]:checked').val();
});
$("#ParkingD").click(function () {
    myObject.ParkingDelivery = $('input[name=ParkingDelivery]:checked').val();
});
$("#FullPackingService").click(function () {
    myObject.FullPackingService = $("#FullPackingService").is(':checked');
});
$("#StorageService").click(function () {
    myObject.StorageService = $("#StorageService").is(':checked');
});
$("#FullStorage").click(function () {
    myObject.HowFullIsStoragePickup = $('input[name=HowFullIsStoragePickup]:checked').val();
});

function OpenModal()
{
    $.get("ReturnTotalCost", myObject, function (data) {
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

