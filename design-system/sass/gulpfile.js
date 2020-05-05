var gulp = require('gulp');
var data = require('gulp-data');
var rename = require('gulp-rename');
var sass = require('gulp-sass');

var log = require('@fizzygalacticus/colored-fancy-log');
const { watch } = require('gulp');

function compileSass(path) {
  var file ='';
  gulp.src('sass/*.scss')
   .pipe(data(function (f) {
     log.info("Compiling theme " + f.path)
   }))
   .pipe(sass({outputStyle: 'compressed'})
       .on('error', sass.logError))
   .pipe(gulp.dest('./public/css'))
   .pipe(rename({ suffix: '.min' }));
};

exports.default = function() {
  const sasswatcher = gulp.watch('**/*.scss', { delay: 500, ignoreInitial: true });
  sasswatcher.on('all', function (path, stats) {
        log.info('Change Sass File ' + path);
        compileSass(path);
    })
};
