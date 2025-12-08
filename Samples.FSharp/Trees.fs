module Trees

open Xunit
open Retroscope

[<Fact>]
let ``Draw a tree`` () = async {
    let! scope = Scope.ConnectDefault
    
    let timestamp = System.DateTime.Now.ToString("HH:mm:ss")
    do! scope.Title($"Draw a tree ({timestamp})")
    do! scope.Clear()

    let rec drawBranch x y angle length thickness depth = async {
        if depth = 0 then
            return ()
        else
            let rad = angle * System.Math.PI / 180.0
            let x2 = x + length * System.Math.Cos(rad)
            let y2 = y + length * System.Math.Sin(rad)

            do! SVG.line x y x2 y2 |> SVG.withStrokeWidth thickness |> scope.Draw

            let newLength = length * 0.7
            let newThickness = thickness * 0.7

            do! drawBranch x2 y2 (angle - 20.0) newLength newThickness (depth - 1)
            do! drawBranch x2 y2 (angle + 30.0) newLength newThickness (depth - 1)
    }

    do! drawBranch 512 1000 -90.0 200.0 8.0 9

    return ()
}

[<Fact>]
let ``Draw a tree with leaves`` () = async {
    let! scope = Scope.ConnectDefault
    
    let timestamp = System.DateTime.Now.ToString("HH:mm:ss")
    do! scope.Title($"Tree with leaves ({timestamp})")
    do! scope.Clear()

    let random = System.Random()
    
    // Helper to create varied green colors using HSL
    let getGreenColor () =
        let hue = 80.0 + random.NextDouble() * 60.0  // 80-140 degrees (yellow-green to cyan-green)
        let saturation = 0.5 + random.NextDouble() * 0.5  // 50-100%
        let lightness = 0.3 + random.NextDouble() * 0.3   // 30-60%
        
        // Convert HSL to RGB
        let c = (1.0 - abs(2.0 * lightness - 1.0)) * saturation
        let h' = hue / 60.0
        let x = c * (1.0 - abs(h' % 2.0 - 1.0))
        let m = lightness - c / 2.0
        
        let (r, g, b) = 
            if h' < 1.0 then (c, x, 0.0)
            elif h' < 2.0 then (x, c, 0.0)
            elif h' < 3.0 then (0.0, c, x)
            elif h' < 4.0 then (0.0, x, c)
            elif h' < 5.0 then (x, 0.0, c)
            else (c, 0.0, x)
        
        let toHex v = int ((v + m) * 255.0)
        sprintf "#%02x%02x%02x" (toHex r) (toHex g) (toHex b)

    let rec drawBranch x y angle length thickness depth = async {
        if depth = 0 then
            return ()
        else
            let rad = angle * System.Math.PI / 180.0
            let x2 = x + length * System.Math.Cos(rad)
            let y2 = y + length * System.Math.Sin(rad)

            do! SVG.line x y x2 y2 |> SVG.withStrokeWidth thickness |> scope.Draw

            // Draw leaves when depth is low (branch ends)
            if depth <= 2 then
                let greenColor = getGreenColor()
                do! SVG.circle x2 y2 (thickness * 5.0) |> SVG.withFill greenColor |> scope.Draw

            let newLength = length * 0.9
            let newThickness = thickness * 0.7

            let angleVariation1 = -20.0 + (random.NextDouble() - 0.5) * 30.0  // -25 to -15
            let angleVariation2 = 20.0 + (random.NextDouble() - 0.5) * 30.0   // 25 to 35

            do! drawBranch x2 y2 (angle + angleVariation1) newLength newThickness (depth - 1)
            do! drawBranch x2 y2 (angle + angleVariation2) newLength newThickness (depth - 1)
    }

    do! drawBranch 512 1000 -90.0 150.0 16.0 9

    return ()
}