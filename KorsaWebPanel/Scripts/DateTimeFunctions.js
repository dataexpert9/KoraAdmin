function ConvertTimeformat(format, str) {
    var time = str;
    var hours = Number(time.match(/^(\d+)/)[1]);
    var minutes = Number(time.match(/:(\d+)/)[1]);
    var AMPM = time.match(/\s(.*)$/)[1];
    if (AMPM == "PM" && hours < 12) hours = hours + 12;
    if (AMPM == "AM" && hours == 12) hours = hours - 12;
    var sHours = hours.toString();
    var sMinutes = minutes.toString();
    if (hours < 10) sHours = "0" + sHours;
    if (minutes < 10) sMinutes = "0" + sMinutes;
    return (sHours + ":" + sMinutes);
}

function convertUTCDateToLocalDate(date) {
    var Datee = date.split("/");
    var Yearr = Datee[2].split(" ");

    //get time

    var Timee = ConvertTimeformat("24", Yearr[1] + " " + Yearr[2]).split(":");

    //year       ,//month       , //date       ,//hh          ,//minutes
    var DateMade = new Date(Yearr[0], Datee[1] - 1, Datee[0], Timee[0], Timee[1]);

    var newDate = new Date(DateMade.getTime() + DateMade.getTimezoneOffset() * 60 * 1000);

    var offset = DateMade.getTimezoneOffset() / 60;
    var hours = DateMade.getHours();

    newDate.setHours(hours - offset);

    return getFormattedDate(newDate);
}

function getFormattedDate(date) {
    var year = date.getFullYear();

    var month = (1 + date.getMonth()).toString();
    month = month.length > 1 ? month : '0' + month;

    var day = date.getDate().toString();
    day = day.length > 1 ? day : '0' + day;

    var hours = date.getHours().toString()
    var minutes = date.getMinutes().toString()

    hours = hours.length > 1 ? hours : '0' + hours
    minutes = minutes.length > 1 ? minutes : '0' + minutes

    suffix = (hours >= 12) ? 'PM' : 'AM';

    hours = (hours > 12) ? hours - 12 : hours;

    //if 00 then it is 12 am
    hours = (hours == '00') ? 12 : hours;

    var time = hours + ':' + minutes + ' ' + suffix;

    return day + '/' + month + '/' + year + ' ' + time;
}