This describes how to build a new version of the monaco-editor-yaml

Found information on these pages

https://stackoverflow.com/questions/50606676/how-do-i-add-a-new-language-syntax-to-monaco-editor
https://github.com/Microsoft/monaco-languages
https://github.com/Microsoft/monaco-editor/blob/master/metadata.js#L68-L70

open the destination folder in VS Code
clone the monaco editor
$ git clone https://github.com/Microsoft/monaco-editor
go to the folder
$ cd monaco-editor

install
~/monaco-editor$ npm install .

test it (optional)
~/monaco-editor$ npm run simpleserver
Open the url (http://127.0.0.1:8088) that is displayed in the terminal. Navigate to 
monaco-editor
test
ctrl + c to stop.

add your own language. In this case monaco-yaml
~/monaco-editor$ npm install monaco-yaml

Open ~/metadata.js in the root and add the references to the monaco-yaml project
Be sure to check if the references to your files are correct. Use existing configuration for comparison.
,
{
	name: 'monaco-yaml',
	contrib: 'vs/language/yaml/monaco.contribution',
	modulePrefix: 'vs/language/yaml',
	paths: {
		src: '/monaco-yaml/dev',
		'npm/dev': 'node_modules/monaco-yaml/dev',
		'npm/min': 'node_modules/monaco-yaml/min',
		esm: 'node_modules/monaco-yaml/esm',
	}
}

Run it
~/monaco-editor$ npm run simpleserver

Register it in the exports.
~/monaco-editor/ci/webpackcongfig.cs
Add the this entry:
"yaml.worker": 'monaco-yeaml/esm/yaml.worker',

Run the release. This gives an error for Yaml:
~/monaco-editor$ npm run release

For Yaml this gives an error we need to fix in the worker.

Non-relative import for unknown module: monaco-editor/esm/vs/editor/editor.worker in <path>\monaco-editor\node_modules\monaco-yaml\esm\yaml.worker.js

Go to that file and change this line:
import * as worker from 'monaco-editor/esm/vs/editor/editor.worker';
to:
import * as worker from 'monaco-editor-core/esm/vs/editor/editor.worker';

That gives the next error:
Non-relative import for unknown module: js-yaml in <path>\monaco-editor\node_modules\monaco-yaml\esm\languageservice\parser\yamlParser04.js

Go to that file and change this line:
import { Schema, Type } from 'js-yaml';
to:
import { Schema, Type } from '../../../../js-yaml';

That gives the next error:
Non-relative import for unknown module: js-yaml in <path>\monaco-editor\node_modules\monaco-yaml\esm\languageservice\parser\yamlParser07.js

Go to that file and change this line:
import { Schema, Type } from 'js-yaml';
to:
import { Schema, Type } from '../../../../js-yaml';

Build the release
~/monaco-editor$ npm run release


The folder ~/monaco-editor/release has been created

In here you will now find the languages in /min/vs/language

!!!!VICTORY!!!!
