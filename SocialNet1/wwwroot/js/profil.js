//на странице профиля отправляем комент на сервер, получаем ответ и рисуем коммент (js пидарас)
async function SendProfileImageCom(divTextId, sender, recipient, imageId, url, comsId, i, j, comCount, color) {

    var text = document.getElementById(divTextId).innerText;

    //var fullurl = url + "?text=" + text + "&sender=" + sender + "&recipient=" + recipient + "&imageId=" + imageId;

    //var promise = await fetch(fullurl);

    const response = await fetch(url, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            sender: sender,
            text: text,
            recipient: recipient,
            imageId: parseInt(imageId)
        })
    });

    var body = await response.json();

    var autIm = body.authorImage;
    var autF = body.authorFormat;
    var autUN = body.authorUserName;
    var autFN = body.authorFirstName;
    var autSN = body.authorSecondName;
    var autCIm = body.authorCoordinatesImage;
    var dt = body.dateTime;
    var com = body.comment;
    //var lc = body.likeCount;
    var lc = 0;

    var html = `<div class="modal__foto_right_comment dark_${color}_border_bottom mid_${color}" id="${i}com${j})">
                                <div class="modal__foto_r_c_l">
                                    <a class="comment_ava_link" href="">
                                        <img class="comment_link_img" src="data:image/${autF};base64,${autIm}" alt="">
                                    </a>
                                </div>
                                <div class="modal__foto_r_c_r">
                                    <div class="comment__name_polit">
                                        <a class="comment__name_link" href="">${autSN} ${autFN}</a>
                                        <img class="comment__polit_img" src="${autCIm}" alt="">
                                    </div>
                                    <div class="comment__all_infa">
                                        <div class="comment__date">${dt}</div>
                                        <div class="comment__icons">
                                            <div class="comment__plus">
                                                <i class="fa fa-commenting-o color_dark_dark_${color}" aria-hidden="true"></i>
                                            </div>
                                            <div id="${i}com${j}_like_on" onclick="LikePlus('${i}com${j}_like_on', '${i}com${j}_like_off', '${i}com${j}_like_num')"
                                                 class="comment__heart">
                                                <i class="fa fa-heart-o color_dark_dark_${color}" aria-hidden="true"></i>
                                            </div>
                                            <div id="${i}com${j}_like_off" onclick="LikeMinus('${i}com${j}_like_on', '${i}com${j}_like_off', '${i}com${j}_like_num')"
                                                 class="comment__heart_bac heart_none">
                                                <i class="fa fa-heart color_dark_dark_${color} heart_none" aria-hidden="true"></i>
                                            </div>
                                            <div id="${i}com${j}_like_num" class="comment__quantity">${lc}</div>
                                        </div>
                                    </div>
                                    <div class="comment__content">${com}</div>
                                </div>
                            </div>`;

    var s0 = $("#" + comsId).children()[0];

    var s1 = s0.children[1].children[0].children[0].children[0];

    var memID = i + "mem" + j;

    s1.id = memID;

    $("#" + memID).append(html);

    var comcount = parseInt(document.getElementById(comCount).innerHTML);

    comcount++;

    document.getElementById(comCount).innerHTML = comcount;

}

//суицид
async function DeleteCom(c, url, user, imageId, comId) {

    var fullurl = url + "?imageAutor=" + user + "&imageId=" + imageId + "&comId=" + comId;

    var promise = await fetch(fullurl);

    var body = await promise.json();

    if (body) {
        var child = document.getElementById(c);
        child.remove();
    }
    else {
        alert("Не получилось удалить комментарий(");
    }

    
}

async function AddFriend(url, user1, user2, addId, deleteId) {

    var fullurl = url + "?username1=" + user1 + "&username2=" + user2;

    var promise = await fetch(fullurl);

    var body = await promise.json();

    if (body) {
        document.getElementById(addId).style = "display:none";
        document.getElementById(deleteId).style = "display:block";
    }

}

async function DeleteFriend(url, user1, user2, addId, deleteId) {

    var fullurl = url + "?username1=" + user1 + "&username2=" + user2;

    var promise = await fetch(fullurl);

    var body = await promise.json();

    if (body) {
        document.getElementById(addId).style = "display:block";
        document.getElementById(deleteId).style = "display:none";
    }
}

async function SetStatus(url, userName, statusId) {

    var text = document.getElementById(statusId).innerText;

    //var fullurl = url + "?text=" + text + "&username=" + userName;

    const response = await fetch(url, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            userName: userName,
            text: text
        })
    });

    var body = await response.json();

    document.getElementById(statusId).innerText = body;
}

// МЕНЯЕМ ЦВЕТ СЕРДЕЧКАМ и цифирку
async function ProfileLikePlus(one, two, num, url, user1, user2, imageid) {

    var fullurl = url + "?username1=" + user1 + "&username2=" + user2 + "&imageid=" + imageid;

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

async function ProfileLikeMinus(one, two, num, url, user1, user2, imageid) {

    var fullurl = url + "?username1=" + user1 + "&username2=" + user2 + "&imageid=" + imageid;

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

async function DeletePhoto(url, imageid, userName, sliderId, photoId) {

    const response = await fetch(url, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            userName: userName,
            imageId: parseInt(imageid)
        })
    });

    var body = await response.json();

    if (body) {
        plusSlides();
        document.getElementById(sliderId).remove();
        document.getElementById(photoId).remove();
    }
    else {
        alert("При удалении фото возникла ошибка(");
    }
}

async function SetAva(url, user, color) {

    var classActive = `dark_dark_${color}_border`;

    var photo = document.getElementsByClassName(classActive)[0];

    if (photo == null) {
        alert("Не выбрано фото(");

        return;
    }

    var avaId = photo.id;

    var ava = avaId.split("-")[1];

    var fullurl = url + "?userName=" + user + "&ava=" + ava;

    var promise = await fetch(fullurl);

    location.href = promise.url;


}

function OpenSetPhoto() {
    var modal = document.getElementById('SetPhoto');

    modal.classList.add('photo_selection__modal__is_open');
}

function CloseSetPhoto() {
    var modal = document.getElementById('SetPhoto');

    modal.classList.remove('photo_selection__modal__is_open');
}

function ChosePhoto(color, imageId) {

    var classActive = `dark_dark_${color}_border`;

    var photos = document.getElementsByClassName("photo_selection__foto_img");

    for (var i = 0; i < photos.length; i++) {
        if (photos[i].classList.contains(classActive))
            photos[i].classList.remove(classActive);
    }

    //var ava = imageId.split("-")[1];

    document.getElementById(imageId).classList.add(classActive);

}