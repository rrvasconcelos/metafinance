# Define pasta raiz dos testes
$testsRoot = Join-Path $PSScriptRoot "tests"
$consolidatedReportPath = Join-Path $testsRoot "coveragereport"

# Limpa relatório consolidado anterior
Remove-Item -Path $consolidatedReportPath -Recurse -Force -ErrorAction SilentlyContinue

# Lista para armazenar caminhos dos arquivos de cobertura
$coverageFiles = @()

# Encontra todos os projetos de teste
$testProjects = Get-ChildItem -Path $testsRoot -Directory

foreach ($testProject in $testProjects) {
    Write-Host "`nExecutando testes para: $($testProject.Name)" -ForegroundColor Cyan
    
    $coveragePath = Join-Path $testProject.FullName "coverage"

    # Limpa e cria diretório de cobertura
    Remove-Item -Path $coveragePath -Recurse -Force -ErrorAction SilentlyContinue
    New-Item -ItemType Directory -Force -Path $coveragePath | Out-Null

    # Executa testes
    dotnet test $testProject.FullName /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput="$coveragePath\"
    
    # Adiciona arquivo de cobertura à lista
    $coverageFile = Get-ChildItem -Path $coveragePath -Filter "coverage.cobertura.xml" -Recurse | Select-Object -First 1
    if ($coverageFile) {
        $coverageFiles += $coverageFile.FullName
    }
}

# Gera relatório consolidado se houver arquivos de cobertura
if ($coverageFiles.Count -gt 0) {
    Write-Host "`nGerando relatório consolidado..." -ForegroundColor Green
    $reportsPath = $coverageFiles -join ";"
    reportgenerator -reports:"$reportsPath" -targetdir:"$consolidatedReportPath" -reporttypes:Html
    Invoke-Item (Join-Path $consolidatedReportPath "index.html")
}
