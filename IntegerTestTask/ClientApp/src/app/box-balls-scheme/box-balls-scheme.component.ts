import { Component, ViewEncapsulation } from '@angular/core';
import * as $ from 'jquery';

@Component({
  selector: 'app-box-balls-scheme',
  templateUrl: './box-balls-scheme.component.html',
  styleUrls: ['./box-balls-scheme.component.css'],
  encapsulation: ViewEncapsulation.None,
})

export class BoxBallsSchemeComponent {
  constructor() {
    $(document).ready(function () {
      // Изменение цвета polygon при наведении на текст, связанный с полигоном по id.
      $('.scheme-item').hover(
        function () {
          $('.scheme polygon[data-id=' + $(this).data('id') + ']').attr('id', 'hover');
          $('.code-table tbody td[data-id=' + $(this).data('id') + ']').addClass('hover');
        },
        function () {
          $('.scheme polygon[data-id=' + $(this).data('id') + ']').attr('id', '');
          $('.code-table tbody td[data-id=' + $(this).data('id') + ']').removeClass('hover');
        }
      );

      // Изменение цвета td таблицы при наведении на poligon, связанный с ним по id.
      $('.scheme polygon').hover(
        function () {
          $('.code-table tbody td[data-id=' + $(this).data('id') + ']').addClass('hover');
        },
        function () {
          $('.code-table tbody td[data-id=' + $(this).data('id') + ']').removeClass('hover');
        }
      );

      // Открытие окна при нажатии на текст, связанный с полигоном по id.
      $('.scheme-item').on('click', function () {
        $('.scheme-popup').hide();
        $('.scheme polygon').removeClass('active');
        $('.scheme-item .scheme-name').removeClass('active');
        $('.code-table tbody td').removeClass('active');

        var popup = $(this).find('.scheme-popup');
        $('.scheme polygon[data-id=' + $(this).data('id') + ']').addClass('active');
        $('.scheme-item[data-id=' + $(this).data('id') + '] .scheme-name').addClass('active');
        $('.code-table tbody td[data-id=' + $(this).data('id') + ']').addClass('active');
        $(popup).show();
      });

      // Открытие окна при нажатии на полигон
      $('.scheme polygon').click(function () {
        $('.scheme-popup').hide();
        $('.scheme-item[data-id=' + $(this).data('id') + ']').trigger('click');
      });

      // Закрытие окон и удаление классов при нажатии вне полигона
      $("body").click(function (e) {
        if ($(e.target).closest(".scheme polygon, .scheme-item").length == 0) {
          $(".scheme-popup").hide();
          $('.scheme polygon').attr('class', '');
          $('.scheme-item .scheme-name').removeClass('active');
          $('.code-table tbody td').removeClass('active');
        }
      });
    })
  }
}
