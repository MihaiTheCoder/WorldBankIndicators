﻿open FSharp.Data
open FSharp.Charting

type WorldBank = WorldBankDataProvider<"World Development Indicators", Asynchronous=false>
let wb = WorldBank.GetDataContext()
let countries = [| wb.Countries.Austria; wb.Countries.Bulgaria; wb.Countries.Hungary; wb.Countries.Serbia; wb.Countries.Ukraine; wb.Countries.Greece; wb.Countries.Romania |]

let draw (indicatorForEachCountry: Runtime.WorldBank.Indicator list, title, yAxisTitle) = 
    indicatorForEachCountry 
        |> List.map (fun i -> Chart.Line(i, i.Code))
        |> Chart.Combine
        |> Chart.WithLegend true
        |> Chart.WithTitle(title)
        |> Chart.WithXAxis(Title="Year", Min=1990.0, Max=2016.0)
        |> Chart.WithYAxis(Title=yAxisTitle)
        |> Chart.Show

[<EntryPoint>]
let main argv = 
    let dieselPumpPrice = [ for c in countries -> c.Indicators.``Pump price for diesel fuel (US$ per liter)`` ]
    
    draw(dieselPumpPrice, "Diesel pump price", "US$")   

    let gdpPerCapita = [ for c in countries -> c.Indicators.``GDP per capita (current US$)`` ]
    
    draw(gdpPerCapita, "GDB Per capita", "US$")

    let gdpPerCapita = [ for c in countries -> c.Indicators.``Birth rate, crude (per 1,000 people)`` ]
    
    draw(gdpPerCapita, "Birth rate year/1000 people", "birth rate in one year for 1000 people")    
    
    printfn "%A" argv
    0 // return an integer exit code
