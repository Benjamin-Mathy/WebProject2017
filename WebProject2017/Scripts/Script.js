function changeValue(str1,str2)
{
    var elem = document.getElementById("StartArrivalDate");
    if (elem.value == str1) elem.value = str2;
    else elem.value = str1;
}