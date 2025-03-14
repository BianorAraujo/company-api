module.exports = function (config) {
    config.set({
      basePath: '',
      frameworks: ['jasmine', '@angular-devkit/build-angular'],
      plugins: [
        require('karma-jasmine'),
        require('karma-chrome-launcher'),
        require('karma-jasmine-html-reporter'),
        require('karma-coverage'),
        require('@angular-devkit/build-angular/plugins/karma')
      ],
      client: {
        jasmine: {},
        clearContext: false
      },
      jasmineHtmlReporter: {
        suppressAll: true
      },
      coverageReporter: {
        dir: require('path').join(__dirname, './coverage'),
        subdir: '.',
        reporters: [
          { type: 'html' },
          { type: 'lcovonly' }, // For Coveralls
          { type: 'text-summary' }
        ]
      },
      reporters: ['progress', 'kjhtml'],
      browsers: ['ChromeHeadless'],
      customLaunchers: {
        ChromeHeadless: {
          base: 'Chrome',
          flags: ['--headless', '--disable-gpu', '--no-sandbox', '--remote-debugging-port=9222']
        }
      },
      singleRun: true, // Ensures tests run once in CI
      restartOnFileChange: false
    });
  };