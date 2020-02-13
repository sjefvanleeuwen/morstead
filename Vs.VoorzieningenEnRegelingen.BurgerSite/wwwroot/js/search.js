(function () {
  var DONE = 4,
    OK = 200,
    searchData,
    searchResult = [],
    query = getParameterByName('q', window.location).toLowerCase(),
    // keywords can be split by spaces
    keywords = query.split(/[\s\-]+/);

  var xhr = new XMLHttpRequest();
  xhr.open('GET', '/search.xml');
  setQueryInTitle();

  xhr.onreadystatechange = function () {
    if (xhr.readyState === DONE) {
      if (xhr.status === OK) {
        // Done loading XML

        searchData = xhr.responseXML.getElementsByTagName('entry');
        for(var i = 0; i < searchData.length; i++) {

          var added = false;

          // Loop over each keyword
          keywords.forEach(function (keyword) {

            if(keyword !== '') {
              // Remove all html tags
              var content = searchData[i].getElementsByTagName('content')[0].textContent.replace(/<[^>]+>/g,"").toLowerCase();
              // Check if the page is not already added to the search results
              if(!added &&
                (searchData[i].getElementsByTagName('title')[0].textContent.toLowerCase().indexOf(keyword) > -1 ||
                content.indexOf(keyword) > -1)) {

                // We've got a match, add it to the results
                searchResult.push({
                  title: searchData[i].getElementsByTagName('title')[0].textContent,
                  url: searchData[i].getElementsByTagName('url')[0].textContent.substring(1),
                  content: content
                });

                added = true;
              }

            }

          });
        }

      } else {
        // Something went wrong...
      }

      parseSearchResults();
    }
  };
  xhr.send(null);

  function parseSearchResults() {
    var tpl = document.getElementById('search-result-template');
    var target = document.getElementById('search-results');
    var ul = document.createElement('ul');


    for(var i = 0; i < searchResult.length; i++) {
      var result = tpl.innerHTML;
      var content = searchResult[i].content;
      var start = -1;

      // Find first occurrence of one of the keywords
      keywords.forEach(function(keyword) {
        var idx = content.indexOf(keyword);
        if(idx > -1 && (start === -1 || idx < start)) {
          // Let the snippet start 100 chars before the keyword
          start = Math.max(0, idx - 100);
        }
      });

      // Create the snippet
      content = content.substr(start, 250);
      // Highlight the matched keyword(s)
      keywords.forEach(function(keyword) {
        content = content.split(keyword).join('<b>' + keyword + '</b>');
      });
      // Add ... to the snippet
      if(start !== 0) {
        content = '&hellip;' + content;
      }
      content += '&hellip;';

      // Replace the placeholders with the contents
      result = result.split('{{title}}').join(searchResult[i].title);
      result = result.split('{{url}}').join(searchResult[i].url);
      result = result.split('{{content}}').join(content);
      ul.innerHTML = result;

      // Add it to the dom
      target.appendChild(ul.getElementsByTagName('li')[0]);
    }
  }

  function setQueryInTitle() {

    var placeholder = document.getElementById('query-placeholder');

    if (placeholder) {
      placeholder.innerHTML = query;
    }
  }

  /**
   * Copied from https://stackoverflow.com/questions/901115/how-can-i-get-query-string-values-in-javascript
   * @param name
   * @param url
   * @return {*}
   */
  function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
      results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
  }
}());