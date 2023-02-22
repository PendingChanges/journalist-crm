using System;

namespace Journalist.Crm.Domain.Articles.DataModels
{
    public record Article(
        string Id,
        string Title,
        DateOnly DeadLineDate,
        int SignNumber,
        ArticleStatus Status,
        bool Published);
}
