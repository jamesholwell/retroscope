namespace Retroscope

module SVG =

    type ISvgNode = 
        abstract member ToSvgString : unit -> string

    type Line(x1: float, y1: float, x2: float, y2: float, ?stroke: string, ?strokeWidth: float) =
        let stroke = defaultArg stroke "#fff"
        let strokeWidth = defaultArg strokeWidth 1.0
        
        interface ISvgNode with
            member _.ToSvgString() =
                $"<line x1=\"{x1}\" y1=\"{y1}\" x2=\"{x2}\" y2=\"{y2}\" stroke=\"{stroke}\" stroke-width=\"{strokeWidth}\" />"
