namespace Retroscope

module SVG =

    type Line = {
        X1: float
        Y1: float
        X2: float
        Y2: float
        Stroke: string
        StrokeWidth: float
    }

    type Circle = {
        Cx: float
        Cy: float
        R: float
        Fill: string
        Stroke: string
        StrokeWidth: float
    }

    type Element =
        | Line of Line
        | Circle of Circle

    let inline line (x1: ^a) (y1: ^b) (x2: ^c) (y2: ^d) = 
        Line {
            X1 = float x1
            Y1 = float y1
            X2 = float x2
            Y2 = float y2
            Stroke = "#fff"
            StrokeWidth = 1.0
        }

    let inline circle (cx: ^a) (cy: ^b) (r: ^c) = 
        Circle {
            Cx = float cx
            Cy = float cy
            R = float r
            Fill = "#fff"
            Stroke = "none"
            StrokeWidth = 0.0
        }

    let withStroke stroke element =
        match element with
        | Line l -> Line { l with Stroke = stroke }
        | Circle c -> Circle { c with Stroke = stroke }

    let withStrokeWidth width element =
        match element with
        | Line l -> Line { l with StrokeWidth = width }
        | Circle c -> Circle { c with StrokeWidth = width }

    let withFill fill element =
        match element with
        | Circle c -> Circle { c with Fill = fill }
        | other -> other

    let toSvgString (element: Element) : string =
        match element with
        | Line line ->
            $"<line x1=\"{line.X1}\" y1=\"{line.Y1}\" x2=\"{line.X2}\" y2=\"{line.Y2}\" stroke=\"{line.Stroke}\" stroke-width=\"{line.StrokeWidth}\" />"

        | Circle circle ->
            let strokeAttr = if circle.Stroke = "none" then "" else $" stroke=\"{circle.Stroke}\" stroke-width=\"{circle.StrokeWidth}\""
            $"<circle cx=\"{circle.Cx}\" cy=\"{circle.Cy}\" r=\"{circle.R}\" fill=\"{circle.Fill}\"{strokeAttr} />"