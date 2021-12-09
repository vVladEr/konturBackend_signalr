# SignalR и Blazor
В этом задании предстоит внедрить использование SignalR и Blazor
в существующий проект.

# Подготовка
Перед началом выполнения задания полезно вспомнить, как в JavaScript происходит обращение к элементам страницы. Также полезно почитать, что такое long polling и web sockets.

# 1. Переход к JavaScript
Перед нами уже знакомый нам проект - BadNews. Владельцы сайта захотели увеличить его посещаемость. В качестве одного из путей достижения своей цели они выбрали добавление комментариев - так они надеются, что люди захотят подольше оставаться на портале. При этом они слышали, что везде происходит переход на JavaScript для рендеринга страниц. Давайте мы постепенно начнём его внедрять.

## 1.1. Подготовка API
Для начала нам нужно подготовить API, откуда будут получаться текущие комментарии для новости. Репозиторий уже готов и лежит в классе `CommentsRepository.cs`. Нужно его зарегистрировать в контейнере.

Создайте новый класс `CommentDto` по пути `Models\Comments`. Поместите туда следующий код:
```cs
namespace BadNews.Models.Comments
{
    public class CommentDto
    {
        public string User { get; set; }
        
        public string Value { get; set; }
    }
}
```
Теперь сделайте общую модель для коллекции комментариев `CommentsDto`:
```cs
using System;
using System.Collections.Generic;
using BadNews.Repositories.Comments;

namespace BadNews.Models.Comments
{
    public class CommentsDto
    {
        public Guid NewsId { get; set; }

        public IReadOnlyCollection<CommentDto> Comments { get; set; }
    }
}
```

Для этого создайте новый класс `CommentsController` из следующего шаблона:
```cs
using System;
using System.Linq;
using BadNews.Models.Comments;
using BadNews.Repositories.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BadNews.Controllers
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentsRepository commentsRepository;

        public CommentsController(CommentsRepository commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        // GET
        [HttpGet("api/news/{id}/comments")]
        public ActionResult<CommentsDto> GetCommentsForNews(Guid newsId)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
```
Метод реализуйте самостоятельно.

## 1.2. JavaScript
Для начала подключим JQuery, чтобы удобнее было делать запросы к API. В проекте она уже есть, осталось подключить на страницу. Откройте `Views\News\FullArticle.html` и добавьте после импортов строчку
```html
<script src="/lib/jquery/dist/jquery.min.js"></script>
```

Затем добавьте блок для комментариев после текста новости:
```html
<div id="#comments" />
```

Теперь сделаем запрос к нашему API и вставим полученные комментарии в код страницы:
```js
<script type="text/javascript">
    $.get(`/api/news/@Model.Article.Id.ToString()/comments`, function(data) {
        const commentsDiv = document.getElementById('comments');
        
        for (const comment of data.comments) {
            const li = document.createElement("li");
            li.textContent = `${comment.user} говорит: ${comment.value}`;
            commentsDiv.appendChild(li);
        }
    });
</script>
```

Теперь попробуйте открыть любую новость и проверьте, что комментарии вставились.