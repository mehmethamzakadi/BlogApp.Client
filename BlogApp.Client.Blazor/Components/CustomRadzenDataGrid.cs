using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen.Blazor;
using Radzen;

namespace BlogApp.Client.Blazor.Components;

public class CustomRadzenDataGrid<T> : RadzenDataGrid<T>
{
    //[Parameter] public IStringLocalizer<App>? Loc { get; set; }
    public CustomRadzenDataGrid() : base()
    {
        base.AllowSorting = true;
        base.AllowFiltering = true;

        base.EqualsText = "Eşittir";
        base.ContainsText = "İçerir";
        base.DoesNotContainText = "İçermez";
        base.StartsWithText = "İle başlar";
        base.EndsWithText = "İle biter";
        base.NotEqualsText = "Eşit değil";
        base.LessThanText = "Küçüktür";
        base.LessThanOrEqualsText = "Küçük eşit";
        base.GreaterThanText = "Büyüktür";
        base.GreaterThanOrEqualsText = "Büyük eşit";
        base.IsNullText = "Boş";
        base.IsNotNullText = "Boş değil";
        base.ApplyFilterText = "Filtreyi uygula";
        base.ClearFilterText = "Filtreyi temizle";

    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        //if (Loc == null) return;

        //base.AndOperatorText = Loc["And"];
        //base.OrOperatorText = Loc["Or"];
        ////...
        //base.FilterText = Loc["Filter"];

    }


}