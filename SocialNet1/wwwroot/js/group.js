
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

        var addLikeCom = "/api/groupimage/addlikecom";
        var deleteLikeCom = "/api/groupimage/deletelikecom";
        var deleteCOm = "/api/groupimage/deletecom";

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
                                            <div class="comment__minus" onclick="DeleteGroupPhotoCom('${imComId}', '${deleteCOm}',
                                                         '${group}', '${imageId}', '${j}')">
                                                    <i class="fa fa-trash-o color_dark_dark_${color}" aria-hidden="true"></i>
                                                </div>
                                            <div id="${i}com${j}_im_like_on" onclick="GroupComLikePlus('${i}com${j}_im_like_on',
                                                    '${i}com${j}_im_like_off', '${i}com${j}_im_like_num', '${addLikeCom}', '${group}',
                                                    '${sender}', '${imageId}', '${j}')"
                                                 class="comment__heart">
                                                <i class="fa fa-heart-o color_dark_dark_${color}" aria-hidden="true"></i>
                                            </div>
                                            <div id="${i}com${j}_im_like_off" onclick="GroupComLikeMinus('${i}com${j}_im_like_on',
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

async function DeleteGroupPhotoCom(c, url, group, imageId, comId) {

    var fullurl = url + "?groupName=" + group + "&imageId=" + imageId + "&comId=" + comId;

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

async function GroupComLikePlus(one, two, num, url, group, user, imageid, comid) {

    const response = await fetch(url, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            groupName: group,
            userName: user,
            imageId: parseInt(imageid),
            comId: parseInt(comid)
        })
    });

    var body = await response.json();

    if (body) {
        document.getElementById(two).classList.remove('heart_none');
        document.getElementById(one).classList.add('heart_none');

        var number = parseInt(document.getElementById(num).innerHTML);
        number++;
        document.getElementById(num).innerHTML = number;
    }
    else {
        alert("Лайк под комментарий не поставился(")
    }


}

async function GroupComLikeMinus(one, two, num, url, group, user, imageid, comid) {

    const response = await fetch(url, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            groupName: group,
            userName: user,
            imageId: parseInt(imageid),
            comId: parseInt(comid)
        })
    });

    var body = await response.json();

    if (body) {
        document.getElementById(one).classList.remove('heart_none');
        document.getElementById(two).classList.add('heart_none');

        var number = parseInt(document.getElementById(num).innerHTML);
        number--;
        document.getElementById(num).innerHTML = number;
    }
    else {
        alert("Лайк под комментарий не поставился(")
    }
}

async function DeleteGroupPost(url, postItem, postId, group) {

    var fullurl = url + "?groupName=" + group + "&postId=" + postId;

    const response = await fetch(fullurl);

    var body = await response.json();

    if (body) {
        document.getElementById(postItem).remove();
    }
    else {
        alert("При удалении фото возникла ошибка(");
    }
}

async function PostGroupLikePlus(one, two, num, url, group, postId, liker) {

    var fullurl = url + "?groupName=" + group + "&postId=" + postId + "&liker=" + liker;

    var promise = await fetch(fullurl);

    var body = await promise.json();

    if (body) {
        document.getElementById(two).classList.remove('heart_none');
        document.getElementById(one).classList.add('heart_none');

        var lp = document.getElementById(one);

        var number = parseInt(document.getElementById(num).innerHTML);
        number++;
        document.getElementById(num).innerHTML = number;
    }
    else {
        alert("Лайк не поставился(");
    }
}

async function PostGroupLikeMinus(one, two, num, url, group, postId, liker) {

    var fullurl = url + "?groupName=" + group + "&postId=" + postId + "&liker=" + liker;

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
        alert("Лайк не поставился(");
    }
}

async function AddCommentToGroupPost(textId, url, group, postId, commenter, i, j, commentCount, comsId, cn, co, cl) {

    var textInput = document.getElementById(textId);

    var text = textInput.innerText;

    const response = await fetch(url, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            userName: group,
            text: text,
            commenter: commenter,
            postId: parseInt(postId)
        })
    });

    var body = await response.json();

    if (body == null) {
        alert("Комментарий не создан(");
        return;
    }

    var pId = body.id;
    var x = body.x;
    var y = body.y;
    var dateTime = body.dateTime;
    var likes = body.likesCount;
    var fn = body.firstName;
    var sn = body.secondName;
    var fP = body.formatPhotoCom;
    var f = body.photoCom;
    var text = SeqHeml(body.text);
   // var text = body.text;
    var color = body.color;
    var helpId = body.helpId;
    var author = body.author;
    var comer = body.commenter;

    var deleteComPost = "/api/group/deletecompost";
    var comLikePlusURL = "/api/group/addlikecompost";
    var comLikeMinusURL = "/api/group/deletelikecompost";

    var html1 = `<div class="profil_modal__foto_right_comment dark_${color}_border_bottom light_${color}" id=${i}com${helpId}>
                            <div class="profil_modal__foto_r_c_l">
                                <a class="profil_comment_ava_link" href="">
                                    <img class="profil_comment_link_img" src="data:image/${fP};base64,${f}" alt="">
                                </a>
                            </div>
                            <div class="profil_modal__foto_r_c_r">
                                <div class="profil_comment__name_polit">
                                    <a class="profil_comment__name_link" href="">${fn} ${sn}</a>
                                    <img class="profil_comment__polit_img" src="/photo/coordinates/${x}d${y}.jpg" alt="">
                                </div>
                                <div class="profil_comment__all_infa">
                                    <div class="profil_comment__date">${dateTime}</div>
                                    <div class="profil_comment__icons">
                                        <div class="profil_comment__minus" onclick="DeleteComGroupPost('${i}com${helpId}', '${deleteComPost}',
                                                   '${author}', '${pId}', '${helpId}', '${i}coms_all_count')">
                                               <i class="fa fa-trash-o color_dark_dark_${color}" aria-hidden="true"></i>
                                            </div>
                                        <div id="${i}com${helpId}_like_on" onclick="GroupPostComLikePlus('${i}com${helpId}_like_on', '${i}com${helpId}_like_off',
                                                    '${i}com${helpId}_like_num', '${comLikePlusURL}', '${author}', '${comer}', '${pId}', '${helpId}')"
                                             class="comment__heart" >
                                            <i class="fa fa-heart-o color_dark_dark_${color}" aria-hidden="true"></i>
                                        </div>
                                        <div id="${i}com${helpId}_like_off" onclick="GroupPostComLikeMinus('${i}com${helpId}_like_on', '${i}com${helpId}_like_off',
                                                    '${i}com${helpId}_like_num', '${comLikeMinusURL}', '${author}', '${comer}', '${pId}', '${helpId}')"
                                             class="comment__heart_bac heart_none">
                                            <i class="fa fa-heart color_dark_dark_${color} heart_none" aria-hidden="true"></i>
                                        </div>
                                        <div id="${i}com${helpId}_like_num" class="comment__quantity">${likes}</div>
                                    </div>
                                </div>
                                <div class="profil_comment__content">${text}</div>
                            </div>
                        </div>`;

    var s0 = $("#" + comsId).children()[0];

    var s1 = s0.children[1].children[0].children[0].children[0];

    var memID = i + "mem" + j;

    s1.id = memID;

    $("#" + memID).append(html1);

    var comCount = parseInt(document.getElementById(commentCount).innerText) + 1;
    document.getElementById(commentCount).innerText = comCount;

    CloseComForm(cn, co, cl);

}

async function DeleteComGroupPost(c, url, group, postId, comId, comCount) {
    var fullurl = url + "?groupName=" + group + "&postId=" + postId + "&comId=" + comId;

    var promise = await fetch(fullurl);

    var body = await promise.json();

    if (body) {
        var child = document.getElementById(c);
        child.remove();

        var number = parseInt(document.getElementById(comCount).innerHTML);
        number--;
        document.getElementById(comCount).innerHTML = number;

    }
    else {
        alert("Не получилось удалить комментарий(");
    }
}

async function GroupPostComLikePlus(one, two, num, url, group, user, postid, comid) {

    const response = await fetch(url, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            groupName: group,
            userName: user,
            postId: parseInt(postid),
            comId: parseInt(comid)
        })
    });

    var body = await response.json();

    if (body) {
        document.getElementById(two).classList.remove('heart_none');
        document.getElementById(one).classList.add('heart_none');

        var number = parseInt(document.getElementById(num).innerHTML);
        number++;
        document.getElementById(num).innerHTML = number;
    }
    else {
        alert("Лайк под комментарий не поставился(")
    }
}

async function GroupPostComLikeMinus(one, two, num, url, group, user, postid, comid) {

    const response = await fetch(url, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            groupName: group,
            userName: user,
            postId: parseInt(postid),
            comId: parseInt(comid)
        })
    });

    var body = await response.json();

    if (body) {
        document.getElementById(one).classList.remove('heart_none');
        document.getElementById(two).classList.add('heart_none');

        var number = parseInt(document.getElementById(num).innerHTML);
        number--;
        document.getElementById(num).innerHTML = number;
    }
    else {
        alert("Лайк под комментарий не убрался(")
    }
}
