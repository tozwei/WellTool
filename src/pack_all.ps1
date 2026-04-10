# 要打包的项目列表
$projects = @(
    "WellTool.Core\WellTool.Core.csproj",
    "WellTool.DB\WellTool.DB.csproj",
    "WellTool.Http\WellTool.Http.csproj",
    "WellTool.Json\WellTool.Json.csproj",
    "WellTool.Crypto\WellTool.Crypto.csproj",
    "WellTool.Jwt\WellTool.Jwt.csproj",
    "WellTool.Log\WellTool.Log.csproj",
    "WellTool.Poi\WellTool.Poi.csproj",
    "WellTool.Extra\WellTool.Extra.csproj",
    "WellTool.Cron\WellTool.Cron.csproj",
    "WellTool.Cache\WellTool.Cache.csproj",
    "WellTool.AI\WellTool.AI.csproj",
    "WellTool.Dfa\WellTool.Dfa.csproj",
    "WellTool.Socket\WellTool.Socket.csproj",
    "WellTool.System\WellTool.System.csproj",
    "WellTool.Script\WellTool.Script.csproj",
    "WellTool.Setting\WellTool.Setting.csproj",
    "WellTool.BloomFilter\WellTool.BloomFilter.csproj",
    "WellTool.Aop\WellTool.Aop.csproj",
    "WellTool.Captcha\WellTool.Captcha.csproj",
    "WellTool.All\WellTool.All.csproj"
)

# 确保输出目录存在
$outputDir = "nupkg"
if (!(Test-Path $outputDir)) {
    New-Item -ItemType Directory -Path $outputDir -Force
}

# 为每个项目执行打包命令
foreach ($project in $projects) {
    Write-Host "Packing $project..."
    dotnet pack $project --output $outputDir
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Failed to pack $project" -ForegroundColor Red
    } else {
        Write-Host "Successfully packed $project" -ForegroundColor Green
    }
}

Write-Host "Packing completed!" -ForegroundColor Yellow
