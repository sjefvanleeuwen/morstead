var gulp = require('gulp');
var git = require('gulp-git');
var run = require('gulp-run-command').default;

gulp.task('clone', function(){
    return git.clone('https://github.com/privacybydesign/irmajs.git', function (err) {
      if (err) throw err;
    });
});

gulp.task('default', async()=> {
    await run('npm install -g webpack')();
    await run('npm run build',{cwd:"./irmajs", ignoreErrors: true})();
  });