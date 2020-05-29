var hoverTextProviders = [];

setHoverText = function (language, range, title, content) {
  provider = window.monaco.languages.registerHoverProvider(language, {
    provideHover: function (model, position) {
      if (
        (position.lineNumber < range.startLineNumber) ||
        (position.lineNumber == range.startLineNumber && position.column < range.startColumn) ||
        (position.lineNumber > range.endLineNumber) ||
        (position.lineNumber == range.endLineNumber && position.column > range.endColumn)
      ) return;
      return {
        contents: [
          { value: title },
          { value: content }
        ]
      }
    }
  }
  );
  hoverTextProviders.push(provider);
}

resetHoverTexts = function () {
  hoverTextProviders.forEach(prov => prov.dispose());
}