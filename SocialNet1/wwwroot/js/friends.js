document.addEventListener('DOMContentLoaded', function () {

  //Пользуемся методом объекта document.
  //querySelectorAll - метод. Позволяет передовать селекторы в формате css и получать DOM элементы, которые соответствуют данным селекторам.
  document.querySelectorAll('.friends__list_item_item').forEach(function (tabsLink) {
    //addEventListener - метод, который позволяет вызывать функцию, при появлении какого-нибудь события
    tabsLink.addEventListener('click', function (event) {
      //Значение переменной path берется из события клика
      //currentTarget - html элемент в который был совершен клик
      //dataset - набор data атрибутов. Атрибуты, которые начинаются в html с "data-", попатают в специальный объект dataset
      const path = event.currentTarget.dataset.path

      //выбираем все эл-ты с классом .tab-content
      document.querySelectorAll('.friends__info_container').forEach(function (tabContent) {
        //у каждого таба удаляем класс tab-content-active
        tabContent.classList.remove('friends__info_container_active')
      })
      //после этого у нас нет ни одного активного таба, и нам нужно показать тот, который хотел пользователь при клике. Выбираем html элемент по атрибуту, на это указывают []. Атрибут data-target должен быть равен значению {path}
      document.querySelector(`[data-target="${path}"]`).classList.add('friends__info_container_active')
    })
  })
})


friends__list_item_item
friends__info_container
friends__info_container_active
