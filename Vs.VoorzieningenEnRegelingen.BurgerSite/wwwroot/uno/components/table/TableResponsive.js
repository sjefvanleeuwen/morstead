var TableResponsive = (function () {
    function TableResponsive(table) {
        this.table = table;
        var columns = table.querySelectorAll('thead tr:first-child th');
        var columnHeadings = [];
        var rows = table.querySelectorAll('tbody tr');
        table.classList.add('table--responsive');
        for (var i = 0; i < columns.length; i++) {
            columnHeadings.push(columns.item(i).textContent);
        }
        for (var i = 0; i < rows.length; i++) {
            var rowCols = rows.item(i).querySelectorAll('td');
            if (rowCols.length === columnHeadings.length) {
                for (var j = 0; j < rowCols.length; j++) {
                    rowCols.item(j).setAttribute('data-col', columnHeadings[j]);
                }
            }
        }
    }
    return TableResponsive;
})();
exports.TableResponsive = TableResponsive;
//# sourceMappingURL=TableResponsive.js.map