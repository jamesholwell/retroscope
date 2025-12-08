using Retroscope;

namespace Samples.CSharp;

public class HelloWorldTests{
    [Fact]
    public async Task HelloWorld() {
        var scope = await Scope.ConnectDefaultAsync();
        await scope.TitleAsync($"Hello, World! ({DateTime.Now:HH:mm:ss})");
    }

    [Fact]
    public async Task DrawTriangle() {
        var scope = await Scope.ConnectDefaultAsync();
        await scope.TitleAsync($"Triangle ({DateTime.Now:HH:mm:ss})");

        var random = new Random();
        var offsetX = random.Next(50, 501);
        var offsetY = random.Next(50, 501);

        var rainbowColors = new[] { "#ff0000", "#ff7f00", "#ffff00", "#00ff00", "#0000ff", "#4b0082", "#9400d3" };
        var fillColor = rainbowColors[random.Next(rainbowColors.Length)];
        var strokeColor = rainbowColors[random.Next(rainbowColors.Length)];
        var lineColor = rainbowColors[random.Next(rainbowColors.Length)];

        await scope.DrawAsync(SVG.Element.NewCircle(new SVG.Circle(50 + offsetX, 110 + offsetY, 8, fillColor, strokeColor, 1)));
        await scope.DrawAsync(SVG.Element.NewCircle(new SVG.Circle(10 + offsetX, 180 + offsetY, 8, fillColor, strokeColor, 1)));
        await scope.DrawAsync(SVG.Element.NewCircle(new SVG.Circle(90 + offsetX, 180 + offsetY, 8, fillColor, strokeColor, 1)));

        await scope.DrawAsync(SVG.Element.NewLine(new SVG.Line(50 + offsetX, 110 + offsetY, 10 + offsetX, 180 + offsetY, lineColor, 3)));
        await scope.DrawAsync(SVG.Element.NewLine(new SVG.Line(10 + offsetX, 180 + offsetY, 90 + offsetX, 180 + offsetY, lineColor, 3)));
        await scope.DrawAsync(SVG.Element.NewLine(new SVG.Line(90 + offsetX, 180 + offsetY, 50 + offsetX, 110 + offsetY, lineColor, 3)));
    }

    [Fact]
    public async Task ClearScreen() {
        var scope = await Scope.ConnectDefaultAsync();
        await scope.TitleAsync(string.Empty);
        await scope.ClearAsync();
    }
}
