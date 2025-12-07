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

        await scope.DrawAsync(new SVG.Line(50, 110, 10, 180, "#fff", 3));
        await scope.DrawAsync(new SVG.Line(10, 180, 90, 180, "#fff", 3));
        await scope.DrawAsync(new SVG.Line(90, 180, 50, 110, "#fff", 3));
    }

    [Fact]
    public async Task ClearScreen() {
        var scope = await Scope.ConnectDefaultAsync();
        await scope.TitleAsync(string.Empty);
        await scope.ClearAsync();
    }
}
