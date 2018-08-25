# HobbiesPortal
Тестовое задание ASP.Net Core Web Api

Приложение реализует небольшой сайт, позволяющий управлять увлечениями пользователей!
В приложении реализована навигация по сайту с помощью меню из 5-ти пунктов - "Главная" (без излишних комментариев), "Информация о сервере" - здесь помещается пространный рассказ о нашем сервере, "Контакты" - образец взят из каких-то пояснений. 
Управление этими пунктами меню идет через контроллер HomeController.
Пункт "Пользователи" - реализован через контроллер UsersController. Здесь можно посмотреть всех пользователей, зарегистрированных в базе данных, добавить пользователя, удалить и редактировать.
Через этот пункт меню осуществляется связь пользователя с его увлечениями. Добавлять/удалять увлечения пользователю можно только через процесс редактирования.
Пункт "Увлечения" - реализован через контроллер HobbiesController. Информация об увлечениях имеет иерархическую структуру. Исходным пунктом являются две таблицы - "Тип Увлечения" и "Название Увлечения" (HobbyType и HobbyName). Таблица "Тип Увлечения" содержит типы увлечений - спорт, туризм, кино ..., таблица "Название Увлечения" содержит конкретные виды увлечений - хоккей, теннис ..., водный, горный ..., боевик, кинокомедия ...
Понятие увлечение возникает в момент соединения типа и названия увлечения (например, спорт – хоккей, туризм – водный, …). Это фиксируется в таблице Hobby.
Связь между пользователем и увлечением фиксируется в таблице UserHobby.
Связь между таблицами реализована через первичные ключи, позволяющие обеспечивать непротиворечивость данных (например, удаление типа увлечения влечет за собой удаление всей цепочки данных).
В качестве СУБД используется (в соответствии с заданием) SqLite. Как известно, эта СУБД реализована в виде файла и может использоваться автономно. Файл находится в попроекте SqLiteRepository, в папке Hobbies_Data.
Доступ к базе прописан через строку подключения, получаемую из файла настроек appsettings.json. Строка подключения содержит относительный путь к файлу и, поэтому, не требует исправлений.
Сам проект построен по принципу разделения функциональности – сам проект, обеспечивающий обработку запросов, выдачу и отображение информации клиенту, модель данных, реализована в виде библиотеки и работа с хранилищем данных (тоже библиотека).
Преимущества такого подхода очевидны – можно заменить модели или репозиторий или даже сам управляющий модуль …
Проект лежит на GitHub по ссылке: https://github.com/ZhoraKrik/HobbiesPortal
Полученный проект можно собрать в Visual Studio 2017.
Тестирование и отладка проводилась на IIS Express. Для развертывания на IIS Express дополнительных действий не требуется – нажимаем кнопку IIS Express и процесс запущен, можно тестировать работу …
Для запуска на IIS Express, в файле launchSettings.json прописан URL запуска по умолчанию – "launchUrl": "api/home/default".
api/home/default – URL главной страницы.
Попытки развернуть приложение на бесплатном хостинге закончились неудачно – я не смог найти такой, а IIS’a у меня нет.
Авторизацию не делал – проект занял больше времени чем я предполагал.
Если потребность есть – сделаю.

 
