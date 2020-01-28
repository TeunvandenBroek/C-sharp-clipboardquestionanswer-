Contributing to the Clipboard answer program
============================================

Setting up your development environment
---------------------------------------

 * Fork `https://github.com/teun25/C-sharp-clipboardquestionanswer-.git` into your own GitHub account. If you already have a fork and moving to a new computer, make sure you update you fork.
 * If you haven't configured your machine with an SSH key that's known to github, then
   follow [GitHub's directions](https://help.github.com/articles/generating-ssh-keys/)
   to generate an SSH key.
 * Clone your forked repo on your local development machine: `git clone git@github.com:<your_name_here>/C-sharp-clipboardquestionanswer-.git`
 * Change into the `C-sharp-clipboardquestionanswer-` directory: `cd C-sharp-clipboardquestionanswer-`
 * Add an upstream to the original repo, so that fetch from the master repository and not your clone: `git remote add upstream git@github.com:teun25/C-sharp-clipboardquestionanswer-.git`
 
 Running the example project
----------------------------

 * Change into the example directory: `cd example`
 * Run the Program: `example run`
 
 Contribute
----------

We really appreciate contributions via GitHub pull requests. To contribute take the following steps:

 * Make sure you are up to date with the latest code on the master: 
   * `git fetch upstream`
   * `git checkout upstream/develop -b <name_of_your_branch>`
 * Apply your changes
 * Verify your changes and fix potential warnings/errors
 * Commit your changes: `git commit -am "<your informative commit message>"`
 * Push changes to your fork: `git push origin <name_of_your_branch>`

Send us your pull request:

 * Go to `https://github.com/teun25/C-sharp-clipboardquestionanswer-` and click the "Compare & pull request" button.

 Please make sure you solved all warnings and errors reported by the static code analyses and that you fill in the full pull request template. Failing to do so will result in us asking you to fix it.
