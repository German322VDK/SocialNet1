

// МЕНЯЕМ ЦВЕТ СЕРДЕЧКАМ и цифирку
async function GroupLikePlus(one, two, num, url, group, user, imageid) {

    var fullurl = url + "?groupname=" + group + "&username=" + user + "&imageid=" + imageid;

    var promise = await fetch(fullurl);

    var body = await promise.json();

    if (body) {
        document.getElementById(two).classList.remove('heart_none');
        document.getElementById(one).classList.add('heart_none');

        var number = parseInt(document.getElementById(num).innerHTML);
        number++;
        document.getElementById(num).innerHTML = number;
    }
    else {
        alert("Лайк не поставился(");
    }

}

async function GroupLikeMinus(one, two, num, url, group, user, imageid) {

    var fullurl = url + "?groupname=" + group + "&username=" + user + "&imageid=" + imageid;

    var promise = await fetch(fullurl);

    var body = await promise.json();

    if (body) {
        document.getElementById(one).classList.remove('heart_none');
        document.getElementById(two).classList.add('heart_none');

        var number = parseInt(document.getElementById(num).innerHTML);
        number--;
        document.getElementById(num).innerHTML = number;
    }
    else {
        alert("Лайк не Убрался(");
    }

}