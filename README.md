# Clipboard Question Answer
## How to use
Simply copy one of the commands to the clipboard, and the application will assist you with a notification.

## How to conttibute
Anyone is welcome to contribute. Feel free to fork and add your own commands.

### Adding to actions
When you add/change existing actions, create a pull request so it can be merged into the master branch of this project.

### Creating new actions
Have an idea for new acitons? Add a new action to the mix. Simply create a class and implement the `IAction` interface. To include it, go to the Bootstrap class and add it to the `ConfigureDependancies()` method. The placement is very important. Where you place it in the stack determins the order if execution.
