import { FileBlob, SpreadsheetFile } from "@oai/artifact-tool";

const source = "C:/Users/Moscaphional/Desktop/temp/移动游戏开发/_AAFinal/Excels/Cards.xlsx";
const blob = await FileBlob.load(source);
const workbook = await SpreadsheetFile.importXlsx(blob);
const summary = await workbook.inspect({
    kind: "workbook,sheet,table,region",
    maxChars: 12000,
    tableMaxRows: 20,
    tableMaxCols: 24,
    tableMaxCellChars: 120
});

console.log(summary.ndjson);
