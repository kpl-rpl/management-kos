param(
    [Parameter(Mandatory = $true)]
    [string]$Name,

    [string]$OutputDir = ".\\Data\\Migrations"
)

function Convert-ToPascalCase {
    param([string]$Text)

    if ([string]::IsNullOrWhiteSpace($Text)) {
        return "Migration"
    }

    $parts = $Text -split "[^a-zA-Z0-9]+" | Where-Object { $_ -ne "" }
    if ($parts.Count -eq 0) {
        return "Migration"
    }

    return ($parts | ForEach-Object {
        if ($_.Length -eq 1) {
            $_.ToUpper()
        }
        else {
            $_.Substring(0, 1).ToUpper() + $_.Substring(1)
        }
    }) -join ""
}

$safeName = ($Name.ToLower() -replace "[^a-z0-9]+", "_" -replace "_{2,}", "_").Trim('_')
if ([string]::IsNullOrWhiteSpace($safeName)) {
    $safeName = "new_migration"
}

$timestamp = Get-Date -Format "yyyyMMddHHmmss"
$fileName = "${timestamp}_${safeName}.sql"

if (-not (Test-Path $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir | Out-Null
}

$fullPath = Join-Path $OutputDir $fileName

$template = @"
-- Migration: $fileName
-- Tulis SQL migration di bawah ini

-- contoh:
-- ALTER TABLE Kamar ADD COLUMN Lantai INT NOT NULL DEFAULT 1;
"@

Set-Content -Path $fullPath -Value $template -Encoding UTF8

Write-Host "Migration file berhasil dibuat:" -ForegroundColor Green
Write-Host $fullPath -ForegroundColor Cyan
