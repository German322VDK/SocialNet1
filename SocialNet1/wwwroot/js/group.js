﻿
async function DeleteGroupPhoto(url, imageid, groupName, sliderId, photoId) {

    const response = await fetch(url, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            groupName: groupName,
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

async function SendGroupImageCom(divTextId, sender, group, imageId, url, comsId, i, j, comCount, color, cmId) {

        var text = document.getElementById(divTextId).innerText;

        //var fullurl = url + "?text=" + text + "&sender=" + sender + "&recipient=" + recipient + "&imageId=" + imageId;

        //var promise = await fetch(fullurl);

        const response = await fetch(url, {
            method: "POST",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
                sender: sender,
                text: text,
                groupName: group,
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
        var hId = body.helpId;
        //var lc = body.likeCount;
        var lc = 0;
        j = hId;
        var imComId = `${i}image_com${j})`

        var addLikeCom = "/api/image/addlikecom";
        var deleteLikeCom = "/api/image/deletelikecom";
        var deleteCOm = "/api/image/deletecom";

        var html1 = `<div class="modal__foto_right_comment dark_${color}_border_bottom mid_${color}" id="${imComId}">
                                <div class="modal__foto_r_c_l">
                                    <a class="comment_ava_link" href="">
                                        <img class="comment_link_img" src="data:image/${autF};base64,${autIm}" alt="">
                                    </a>
                                </div>
                                <div class="modal__foto_r_c_r">
                                    <div class="comment__name_polit">
                                       <a class="comment__name_link" href="">${autSN} ${autFN}</a>
                                        <img class="comment__polit_img" src="/${autCIm}" alt="">
                                    </div>
                                    <div class="comment__all_infa">
                                        <div class="comment__date">${dt}</div>
                                        <div class="comment__icons">
                                            <div class="comment__minus" onclick="DeletePhotoCom('${imComId}', '${deleteCOm}',
                                                         '${group}', '${imageId}', '${j}')">
                                                    <i class="fa fa-trash-o color_dark_dark_${color}" aria-hidden="true"></i>
                                                </div>
                                            <div id="${i}com${j}_im_like_on" onclick="ProfileComLikePlus('${i}com${j}_im_like_on',
                                                    '${i}com${j}_im_like_off', '${i}com${j}_im_like_num', '${addLikeCom}', '${group}',
                                                    '${sender}', '${imageId}', '${j}')"
                                                 class="comment__heart">
                                                <i class="fa fa-heart-o color_dark_dark_${color}" aria-hidden="true"></i>
                                            </div>
                                            <div id="${i}com${j}_im_like_off" onclick="ProfileComLikeMinus('${i}com${j}_im_like_on',
                                                        '${i}com${j}_im_like_off', '${i}com${j}_im_like_num', '${deleteLikeCom}',
                                                        '${group}', '${sender}', '${imageId}', '${j}')"
                                                 class="comment__heart_bac heart_none">
                                                <i class="fa fa-heart color_dark_dark_${color} heart_none" aria-hidden="true"></i>
                                            </div>
                                            <div id="${i}com${j}_im_like_num" class="comment__quantity">0</div>
                                        </div>
                                    </div>
                                    <div class="comment__content">${com}</div>
                                </div>
                            </div>`;

        var s0 = $("#" + comsId).children()[0];

        var s1 = s0.children[1].children[0].children[0].children[0];

        var memID = i + "mem" + j;

        s1.id = memID;

        $("#" + memID).append(html1);

        var comcount = parseInt(document.getElementById(comCount).innerHTML);

        comcount++;

        document.getElementById(comCount).innerHTML = comcount;
}