// INICIJALIZACIJA KALENDARA
// eventData je niz datuma (u njega ubacamo datume koji su zauzeti)
// plava boja je trenutni datum, crvena boja je zauzeti datum (moze da se menja ako ne odgovara)
var eventData  = [];

$(document).ready(function () {
    for (var i = 0; i < takenDates.length; i++)
    {
        var obj = { "date": takenDates[i], "badge": true };
        eventData.push(obj);
    }

    $("#my-calendar").zabuto_calendar({
        language: "en", cell_border: true,
        today: true,
        show_days: true,
        weekstartson: 0,
        data: eventData,
        action: function () {
            return myDateFunction(this.id, false);
        }
    });
});


function myDateFunction(id, fromModal) {

    $("#date-popover").hide("slow");
    $("#timeofday").hide("slow");


    if (fromModal) {
        $("#" + id + "_modal").modal("hide");
    }
    var date = $("#" + id).data("date");
    var hasEvent = $("#" + id).data("hasEvent");
    if (hasEvent && !fromModal) {
        return false;
    }

    if (lessDate(date, todayDate))
    {
        $("#date-popover-content").html('You cant select pick up date before today.<hr>');
        $("#timeofday").html("");
    }
    else
    {
        $("#date-popover-content").html('Selected Pick Up Date: ' + date + "<hr>");
        $("#hidden").html('<input type="hidden" name="estimateDate" value=' + date + '>');
        var index = inList(date, partialyTakenDates);
        if (index != -1) {
            var isDis = 0;
            var isChe = 0;
            var htmlContent = '<div class="col-md-4 col-sm-4"> <div class="radio"> <label><input type="radio" name="estimateTime" value="0"';
            if (morning[index] == 1) {
                htmlContent += 'disabled ';
                isDis = 1;
            }
            else
            {
                isChe = 1;
            }
            htmlContent += 'checked > Morning </label > </div > </div > '
                + ' <div class="col-md-4 col-sm-4"> <div class="radio"> <label><input type="radio" name="estimateTime" value="1"';
            if (isDis == 1 && isChe == 0)
                htmlContent += 'checked ';
            if (afternoon[index] == 1) {
                htmlContent += 'disabled ';
                isDis = 1;
            }
            else
            {
                isChe = 1;
            }
            htmlContent += '> Before Noon </label > </div > </div > '
                + ' <div class="col-md-4 col-sm-4"> <div class="radio"> <label><input type="radio" name="estimateTime" value="2"';
            if (isDis == 1 && isChe == 0)
                htmlContent += 'checked ';
            if (lateAfternoon[index] == 1) {
                htmlContent += 'disabled ';
            }
            htmlContent += '> Afternoon </label > </div > </div > ';
            $("#timeofday").html(htmlContent);
        }
        else
        {
            $("#timeofday").html('<div class="col-md-4 col-sm-4"> <div class="radio"> <label><input type="radio" name="estimateTime" value="0" checked> Morning </label> </div> </div>'
                + ' <div class="col-md-4 col-sm-4"> <div class="radio"> <label><input type="radio" name="estimateTime" value="1"> Before Noon </label> </div> </div> '
                + ' <div class="col-md-4 col-sm-4"> <div class="radio"> <label><input type="radio" name="estimateTime" value="2"> Afternoon </label> </div> </div>');
        }
    }

    $("#date-popover").slideDown("slow");
    $("#timeofday").slideDown("fast");
    return true;
}

function lessDate(date, date2)
{
    var year1 = parseInt(date.substring(0, 4));
    var year2 = parseInt(date2.substring(0, 4));
    var month1 = parseInt(date.substring(5, 7));
    var month2 = parseInt(date2.substring(5, 7));
    var day1 = parseInt(date.substring(8, 10));
    var day2 = parseInt(date2.substring(8, 10));

    if (year1 < year2) return true;
    else if (year1 == year2 && month1 < month2) return true;
    else if (year1 == year2 && month1 == month2 && day1 <= day2) return true;

    return false;
}

function inList(date, partialyTakenDates)
{
    for (var i = 0; i < partialyTakenDates.length; i++)
    {
        if (date == partialyTakenDates[i])
            return i;
    }

    return -1;
}

function forDate(date, time)
{
    $("#date-popover").hide("slow");
    $("#timeofday").hide("slow");

    if (lessDate(date, todayDate)) {
        $("#date-popover-content").html('You cant select pick up date before today.<hr>');
        $("#timeofday").html("");
    }
    else {
        $("#date-popover-content").html('Selected Pick Up Date: ' + date + "<hr>");
        $("#hidden").html('<input type="hidden" name="estimateDate" value=' + date + '>');
        var index = inList(date, partialyTakenDates);
        if (index != -1) {
            var isDis = 0;
            var isChe = 0;
            var htmlContent = '<div class="col-md-4 col-sm-4"> <div class="radio"> <label><input type="radio" name="estimateTime" value="0"';
            if (morning[index] == 1) {
                htmlContent += 'disabled ';
                isDis = 1;
            }
            else {
                isChe = 1;
            }
            if (time == 0)
                htmlContent += 'checked > Morning </label > </div > </div > '
                    + ' <div class="col-md-4 col-sm-4"> <div class="radio"> <label><input type="radio" name="estimateTime" value="1"';
            else
                htmlContent += '> Morning </label > </div > </div > '
                    + ' <div class="col-md-4 col-sm-4"> <div class="radio"> <label><input type="radio" name="estimateTime" value="1"';
            if (afternoon[index] == 1) {
                htmlContent += 'disabled ';
                isDis = 1;
            }
            else {
                isChe = 1;
            }
            if (date == 1)
                htmlContent += ' checked ';
            htmlContent += '> Before Noon </label > </div > </div > '
                + ' <div class="col-md-4 col-sm-4"> <div class="radio"> <label><input type="radio" name="estimateTime" value="2"';
            if (lateAfternoon[index] == 1) {
                htmlContent += 'disabled ';
            }
            if (date == 2)
                htmlContent += ' checked ';
            htmlContent += '> Afternoon </label > </div > </div > ';
            $("#timeofday").html(htmlContent);
        }
        else {
            var htmlContent = '<div class="col-md-4 col-sm-4"> <div class="radio"> <label><input type="radio" name="estimateTime" value="0"';
            if (time == 0)
                htmlContent += ' checked ';
            htmlContent += '> Morning </label > </div > </div > '
                + ' <div class="col-md-4 col-sm-4"> <div class="radio"> <label><input type="radio" name="estimateTime" value="1"';
            if (time == 1)
                htmlContent += ' checked ';
            htmlContent += '> Before Noon </label > </div > </div > '
                + ' <div class="col-md-4 col-sm-4"> <div class="radio"> <label><input type="radio" name="estimateTime" value="2"';
            if (time == 2)
                htmlContent += ' checked ';
            htmlContent += '> Afternoon </label > </div > </div > '
            $("#timeofday").html(htmlContent);
        }
    }

    $("#date-popover").slideDown("slow");
    $("#timeofday").slideDown("fast");
    return true;
}