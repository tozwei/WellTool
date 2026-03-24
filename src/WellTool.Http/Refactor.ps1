# WellTool.Http 重构脚本
# 批量重命名冲突的方法

$ErrorActionPreference = "Stop"
Set-Location "d:\Work\WellTool\src\WellTool.Http\Http"

Write-Host "=== 开始重构 WellTool.Http ===" -ForegroundColor Green

# 1. 修复 HttpBase.cs
Write-Host "`n[1/5] 修复 HttpBase.cs..." -ForegroundColor Yellow
$content = Get-Content HttpBase.cs -Raw

# Header 相关方法重命名
$content = $content -replace 'public string\? Header\(string name\)', 'public string? GetHeader(string name)'
$content = $content -replace 'public List<string>\? HeaderList\(string name\)', 'public List<string>? GetHeaderList(string name)'
$content = $content -replace 'public string\? Header\(Header name\)', 'public string? GetHeader(Header name)'

# 已修复的方法保持不变（GetIsHeaderAggregated, GetHttpVersion, GetCharset, SetCharset, GetAllHeaders）

Set-Content HttpBase.cs $content
Write-Host "✓ HttpBase.cs 修复完成" -ForegroundColor Green

# 2. 修复 HttpRequest.cs  
Write-Host "`n[2/5] 修复 HttpRequest.cs..." -ForegroundColor Yellow
$content = Get-Content HttpRequest.cs -Raw

# Method 属性和方法冲突 - 重命名方法为 SetMethod
$content = $content -replace 'public T Method\(Method method\)', 'public T SetMethod(Method method)'

# ContentType 方法重命名为 SetContentType
$content = $content -replace 'public T ContentType\(string contentType\)', 'public T SetContentType(string contentType)'

# 更新所有调用点
$content = $content -replace '\.Method\(', '.SetMethod('
$content = $content -replace '\.ContentType\(', '.SetContentType('
$content = $content -replace '\.GetAllHeaders\(\)', '.GetAllHeaders()'
$content = $content -replace '\.GetCharset\(\)', '.GetCharset()'

Set-Content HttpRequest.cs $content
Write-Host "✓ HttpRequest.cs 修复完成" -ForegroundColor Green

# 3. 修复 HttpResponse.cs
Write-Host "`n[3/5] 修复 HttpResponse.cs..." -ForegroundColor Yellow
$content = Get-Content HttpResponse.cs -Raw

# 更新调用点
$content = $content -replace '\.Header\(', '.GetHeader('
$content = $content -replace '\.GetHeader\(', '.GetHeader('

Set-Content HttpResponse.cs $content
Write-Host "✓ HttpResponse.cs 修复完成" -ForegroundColor Green

# 4. 修复 HttpUtil.cs
Write-Host "`n[4/5] 修复 HttpUtil.cs..." -ForegroundColor Yellow
$content = Get-Content HttpUtil.cs -Raw

# Charset 是属性，不能调用
$content = $content -replace 'http\.Charset\(', 'http.Charset = Encoding.GetEncoding('
$content = $content -replace 'customCharset\)', 'customCharset ?? Encoding.UTF8)'

Set-Content HttpUtil.cs $content
Write-Host "✓ HttpUtil.cs 修复完成" -ForegroundColor Green

# 5. 修复其他文件中的调用
Write-Host "`n[5/5] 修复其他文件..." -ForegroundColor Yellow

# 查找并替换所有 .Header( 调用为 .GetHeader(
Get-ChildItem *.cs -Recurse | ForEach-Object {
    $file = $_.FullName
    $content = Get-Content $file -Raw
    
    if ($content -match '\.Header\(') {
        $content = $content -replace '\.Header\(', '.GetHeader('
        Set-Content $file $content
        Write-Host "  ✓ 修复 $($_.Name)" -ForegroundColor Gray
    }
}

Write-Host "`n=== 重构完成！===`n" -ForegroundColor Green
Write-Host "请运行以下命令验证编译:" -ForegroundColor Cyan
Write-Host "dotnet build ..\WellTool.Http.csproj`n" -ForegroundColor White
