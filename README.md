# TableNet
Blazor DataTable Component ([Demo](https://pablofrommars.github.io/covid19))

### Example

![example](Demo/wwwroot/img/example.png)

```razor
<Table Loader="Loader" Context="wf" Dark="true" Rounded="true">
    <Loading><p>Loading...</p></Loading>
    <Header>
        <Column Field="Date" Sortable="true" />
        <Column Field="TemperatureC" Label="Temp. (C)" Sortable="true" />
        <Column Field="TemperatureF" Label="Temp. (F)" Sortable="true" />
        <Column Field="Summary" Filterable="true" />
    </Header>
    <Row>
        <Cell>@wf.Date.ToShortDateString()</Cell>
        <Cell Align="Align.Right" >@wf.TemperatureC</Cell>
        <Cell Align="Align.Right">@wf.TemperatureF</Cell>
        <Cell Align="Align.Center">@wf.Summary</Cell>
    </Row>
</Table>

@code {
    private async Task<IList<WeatherForecast>> Loader() => ...;
}
```
