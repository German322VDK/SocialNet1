document.addEventListener('DOMContentLoaded', function () {

  //Пользуемся методом объекта document.
  //querySelectorAll - метод. Позволяет передовать селекторы в формате css и получать DOM элементы, которые соответствуют данным селекторам.
  document.querySelectorAll('.profil_left__news_dif_btn').forEach(function (tabsLink) {
    //addEventListener - метод, который позволяет вызывать функцию, при появлении какого-нибудь события
    tabsLink.addEventListener('click', function (event) {
      //Значение переменной path берется из события клика
      //currentTarget - html элемент в который был совершен клик
      //dataset - набор data атрибутов. Атрибуты, которые начинаются в html с "data-", попатают в специальный объект dataset
      const path = event.currentTarget.dataset.path

      //выбираем все эл-ты с классом .tab-content
      document.querySelectorAll('.profil__post_profil_container').forEach(function (tabContent) {
        //у каждого таба удаляем класс tab-content-active
        tabContent.classList.remove('profil__post_profil_container_active')
      })
      //после этого у нас нет ни одного активного таба, и нам нужно показать тот, который хотел пользователь при клике. Выбираем html элемент по атрибуту, на это указывают []. Атрибут data-target должен быть равен значению {path}
      document.querySelector(`[data-target="${path}"]`).classList.add('profil__post_profil_container_active')
    })
  })

    if (document.getElementById('botton1') != null && document.getElementById('botton2') != null && document.getElementById('botton3') != null) {
        document.getElementById('botton1').onclick = function () {
            document.getElementById('botton2').classList.remove('dark_blue_border_bottom')
            document.getElementById('botton3').classList.remove('dark_blue_border_bottom')
            document.getElementById('botton1').classList.add('dark_blue_border_bottom')
        }

        document.getElementById('botton2').onclick = function () {
            document.getElementById('botton1').classList.remove('dark_blue_border_bottom')
            document.getElementById('botton3').classList.remove('dark_blue_border_bottom')
            document.getElementById('botton2').classList.add('dark_blue_border_bottom')
        }

        document.getElementById('botton3').onclick = function () {
            document.getElementById('botton1').classList.remove('dark_blue_border_bottom')
            document.getElementById('botton2').classList.remove('dark_blue_border_bottom')
            document.getElementById('botton3').classList.add('dark_blue_border_bottom')
        }
    }

    if (document.getElementById('profil_left__btn_one') != null && document.getElementById('profil_left__btn_two') != null ) {

        document.getElementById('profil_left__btn_one').onclick = function () {
            document.getElementById('profil_left_dop_infa').classList.add('profil_left_dop_infa_active')
            document.getElementById('profil_left__btn_two').classList.add('profil_left__btn_active')
            document.getElementById('profil_left__btn_one').classList.remove('profil_left__btn_active')
        }

        document.getElementById('profil_left__btn_two').onclick = function () {
            document.getElementById('profil_left_dop_infa').classList.remove('profil_left_dop_infa_active')
            document.getElementById('profil_left__btn_two').classList.remove('profil_left__btn_active')
            document.getElementById('profil_left__btn_one').classList.add('profil_left__btn_active')
        }
    }

    if (document.getElementById('modal_foto__close') != null && document.getElementById('modal__foto_one') != null) {

        // ЗАКРЫВАЕМ ФОРМУ С ФОТО
        document.getElementById('modal_foto__close').onclick = function () {
            document.getElementById('modal__foto_one').classList.remove('modal_1__is_open')
        }
       
    }

    if (document.getElementById('modal__foto_r_r_btn_circle') != null && document.getElementById('circle__tooltop') != null) {

        // ОТКРЫВАЕМ СПИСОК РЕПОСТНУВШИХ
        document.getElementById('modal__foto_r_r_btn_circle').onclick = function () {
            document.getElementById('circle__tooltop').classList.toggle('circle__tooltop_active')
        }
    }


})
 