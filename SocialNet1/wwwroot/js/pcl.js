function OpenComForm(ComTwo, ComOld, clAct){

    var cOld = document.getElementById(ComOld);
    var cTwo = document.getElementById(ComTwo);

    cOld.classList.add('display_none');
    cTwo.classList.add(clAct); //modal__foto_right_my_comment_write_active , 'modal__foto_right_my_comment_write_active'
  }
  // ЗАКРЫВАЕМ ФОРМУ С НАПИСАНИЕМ КОМЕНТА
  function CloseComForm(ComTwo, ComOld, clAct){
    var cOld = document.getElementById(ComOld);
    var cTwo = document.getElementById(ComTwo);
    cOld.classList.remove('display_none')
    cTwo.classList.remove(clAct)
  }

  //суицид
  function DeleteCom(c){
    var child = document.getElementById(c);
    child.remove();
  }

  // МЕНЯЕМ ЦВЕТ СЕРДЕЧКАМ
function LikePlus(one, two, num){

  document.getElementById(two).classList.remove('heart_none');
  document.getElementById(one).classList.add('heart_none');

  var number = parseInt(document.getElementById(num).innerHTML);
  number++;
  document.getElementById(num).innerHTML = number;

  }

  function LikeMinus(one, two, num){
  document.getElementById(one).classList.remove('heart_none');
  document.getElementById(two).classList.add('heart_none');

  var number = parseInt(document.getElementById(num).innerHTML);
  number--;
  document.getElementById(num).innerHTML = number;
  }

  // ОТКРЫВАЕМ И ЗАКРЫВАЕМ РЕПОСТЫ

function ShowReps(id){
  var reps = document.getElementById(id);
  reps.classList.toggle('circle__tooltop_active');
  }

function fun__but(element) {

    var text = document.getElementById(element); //"postadd"
    if (text.style.display === "none") {
        text.style.display = "block";
    }
    else {
        text.style.display = "none";
    }
}

init();

// function fun__but(element) {

//     var text = document.getElementById(element); //"postadd"
//     if (text.style.display === "none") {
//         text.style.display = "block";
//     }
//     else {
//         text.style.display = "none";
//     }
// }

function init(){
    var elements = document.getElementsByClassName('jsrepost');

    for(var i = 0; i < elements.length; i++)
        elements[i].style.display = "none";
}

function repost(element){
    var text = document.getElementById(element); //"postadd"

    var pidaras_display = text.style.display;

    if (text.style.display == "none") {
        text.style.display = "block";
    }
    else {
        text.style.display = "none";
    }

    var newStyle = document.getElementById(element).style.display;

    var elements = document.getElementsByClassName('jsrepost');

    for(var i = 0; i < elements.length; i++)
    {
        var textid = text.id;
        var elId = elements[i].id;
        if(textid != elId){
            var elStyle = elements[i].style.display;
            if (elements[i].style.display == "block") {
                elements[i].style.display = "none";
            }
        }
    }

    var oldStyle = document.getElementById(element).style.display;

}


async function SendCom(divTextId, sender, recipient, imageId, url, comsId, i, j) {

    var text = document.getElementById(divTextId).innerText;

    var fullurl = url + "?text=" + text + "&sender=" + sender + "&recipient=" + recipient + "&imageId=" + imageId;

    var promise = await fetch(fullurl);

    var body = await promise.json();

    if (body == null)
        return;

    var autIm = body.authorImage;
    var autF = body.authorFormat;
    var autUN = body.authorUserName;
    var autFN = body.authorFirstName;
    var autSN = body.authorSecondName;
    var autCIm = body.authorCoordinatesImage;
    var dt = body.dateTime;
    var com = body.comment;
    var lc = body.likeCount;

    var html = `<div class="modal__foto_right_comment dark_blue_border_bottom" id="${i}com${j})">
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
                                                <i class="fa fa-commenting-o color_dark_dark_blue" aria-hidden="true"></i>
                                            </div>
                                            <div id="${i}com${j}_like_on" onclick="LikePlus('${i}com${j}_like_on', '${i}com${j}_like_off', '${i}com${j}_like_num')"
                                                 class="comment__heart">
                                                <i class="fa fa-heart-o color_dark_dark_blue" aria-hidden="true"></i>
                                            </div>
                                            <div id="${i}com${j}_like_off" onclick="LikeMinus('${i}com${j}_like_on', '${i}com${j}_like_off', '${i}com${j}_like_num')"
                                                 class="comment__heart_bac heart_none">
                                                <i class="fa fa-heart color_dark_dark_blue heart_none" aria-hidden="true"></i>
                                            </div>
                                            <div id="${i}com${j}_like_num" class="comment__quantity">${lc}</div>
                                        </div>
                                    </div>
                                    <div class="comment__content">${com}</div>
                                </div>
                            </div>`;

    var s0 = $("#" + comsId).children()[0];

    var s1 = s0.children[1];

    var s2 = s1.children[0];

    var s3 = s2.children[0];

    var s4 = s3.children[0];

    var memID = i + "mem" + j;

    s4.id = memID;

    $("#" + memID).append(html);

    //$("#"+comsId).append(html);

    //var parent = document.getElementById(comsId);

    //parent.innerHTML = html;

    var lol = 1;
}


// //ОТКРЫВАЕМ ФОРМУ С НАПИСАНИЕМ КОМЕНТА
// function OpenComFormN(ComTwo, ComOld){

//     var cOld = document.getElementById(ComOld);
//     var cTwo = document.getElementById(ComTwo);
    
//     cOld.classList.add('display_none');
//     cTwo.classList.add('news_modal__foto_right_my_comment_write_active');
//     }

//     // ЗАКРЫВАЕМ ФОРМУ С НАПИСАНИЕМ КОМЕНТА
//     function CloseComFormN(ComTwo, ComOld){
//     var cOld = document.getElementById(ComOld);
//     var cTwo = document.getElementById(ComTwo);
//     cOld.classList.remove('display_none');
//     cTwo.classList.remove('news_modal__foto_right_my_comment_write_active');
//     }
