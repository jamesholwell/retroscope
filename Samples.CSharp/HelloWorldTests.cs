using Retroscope;

namespace Samples.CSharp;

public class HelloWorldTests{
    [Fact]
    public async Task HelloWorld() {
        var scope = await Scope.ConnectDefaultAsync();
        await scope.TitleAsync($"Hello, World! ({DateTime.Now:HH:mm:ss})");
    }
}
