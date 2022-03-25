//на странице профиля отправляем комент на сервер, получаем ответ и рисуем коммент (js пидарас)
async function SendProfileImageCom(divTextId, sender, recipient, imageId, url, comsId, i, j, comCount, color) {

    var text = document.getElementById(divTextId).innerText;

    var fullurl = url + "?text=" + text + "&sender=" + sender + "&recipient=" + recipient + "&imageId=" + imageId;

    var promise = await fetch(fullurl);

    var body = await promise.json();

    var autIm = body.authorImage;
    var autF = body.authorFormat;
    var autUN = body.authorUserName;
    var autFN = body.authorFirstName;
    var autSN = body.authorSecondName;
    var autCIm = body.authorCoordinatesImage;
    var dt = body.dateTime;
    var com = body.comment;
    var lc = body.likeCount;

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

    var fullurl = url + "?text=" + text + "&username=" + userName;

    var promise = await fetch(fullurl);

    var body = await promise.json();

    document.getElementById(statusId).innerText = body;
}