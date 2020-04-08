[![License](https://img.shields.io/github/license/BlazorExtensions/Storage.svg?longCache=true&style=flat-square)](https://github.com/pablofrommars/TableNet/blob/master/LICENSE)
[![Package Version](https://img.shields.io/badge/nuget-v1.0.8-blue.svg?longCache=true&style=flat-square)](https://www.nuget.org/packages/Table.Net/1.0.8)

# Table.Net

Blazor DataTable Component ([Demo](https://pablofrommars.github.io/covid19))

![example](Demo/wwwroot/img/example.png)

## Features

* Flexible design
* Sorting
* Filtering
* Tooltip
* Asynchronous

## Install

* Add Table.Net [nuget package](https://www.nuget.org/packages/Table.Net)
* Reference static assets: ```<link href="_content/Table.Net/css/main.css" rel="stylesheet" />```

## Dependencies

* [Bootstrap v4](https://getbootstrap.com/)
* [Font Awesome](https://fontawesome.com/)

*libman.json sample:*
```json
{
  "version": "1.0",
  "defaultProvider": "cdnjs",
  "libraries": [
    {
      "library": "twitter-bootstrap@4.4.1",
      "destination": "wwwroot/lib/twitter-bootstrap/",
      "files": [
        "css/bootstrap.min.css"
      ]
    },
    {
      "library": "font-awesome@5.12.1",
      "destination": "wwwroot/lib/font-awesome/",
      "files": [
        "css/all.min.css",
        "webfonts/fa-brands-400.eot",
        "webfonts/fa-brands-400.svg",
        "webfonts/fa-brands-400.ttf",
        "webfonts/fa-brands-400.woff",
        "webfonts/fa-brands-400.woff2",
        "webfonts/fa-regular-400.eot",
        "webfonts/fa-regular-400.svg",
        "webfonts/fa-regular-400.ttf",
        "webfonts/fa-regular-400.woff",
        "webfonts/fa-regular-400.woff2",
        "webfonts/fa-solid-900.eot",
        "webfonts/fa-solid-900.svg",
        "webfonts/fa-solid-900.ttf",
        "webfonts/fa-solid-900.woff",
        "webfonts/fa-solid-900.woff2"
      ]
    }
  ]
}
```

## Sample

```razor
@using Table.Net

<Table Loader=Loader Context="wf" Dark=true Rounded=true>
    <Loading><p>Loading...</p></Loading>
    <Header>
        <Column Field="Date" Sortable=true Sort="-1" />
        <Column Field="TemperatureC" Label="Temp. (C)" Sortable=true />
        <Column Field="TemperatureF" Label="Temp. (F)" Sortable=true />
        <Column Field="Summary" Filterable=true />
    </Header>
    <Row>
        <Cell>@wf.Date.ToShortDateString()</Cell>
        <Cell Align=Align.Right >@wf.TemperatureC</Cell>
        <Cell Align=Align.Right>@wf.TemperatureF</Cell>
        <Cell Align=Align.Center>@wf.Summary</Cell>
    </Row>
</Table>

@code {
    private async Task<IList<WeatherForecast>> Loader() => ...;
}
```

## Styling Parameters

| Parameter | Default | Bootstrap |
|:----------|:-------:|----------:|
|Small|true|[table-sm](https://getbootstrap.com/docs/4.4/content/tables/#small-table)|
|Dark|false|[table-dark](https://getbootstrap.com/docs/4.4/content/tables/#examples)|
|HeadDark|false|[thead-dark](https://getbootstrap.com/docs/4.4/content/tables/#table-head-options)|
|Striped|false|[table-striped](https://getbootstrap.com/docs/4.4/content/tables/#striped-rows)|
|Hover|false|[table-hover](https://getbootstrap.com/docs/4.4/content/tables/#hoverable-rows)|
|Rounded|false|[rounded](https://getbootstrap.com/docs/4.4/utilities/borders/#border-radius)|

## Interactivity

* OnRowClick: ```EventCallback<TItem>```

## Columns

| Parameter | Default |  Description |
|:----------|:-------:|:--|
|Field||Name of property in `TItem`|
|Label||Header override, `Field` is used by default|
|Sortable| false | Allows sorting |
|Filterable| false | Allows filtering |
| Sort | 0 | Indicates default sorting (-1 for descending, 1 for ascending) |
| Tooltip || Information text displayed on column hover |
| TooltipPlacement | center | left, center or right |