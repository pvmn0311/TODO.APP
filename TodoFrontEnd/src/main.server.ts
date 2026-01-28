import { BootstrapContext, bootstrapApplication } from '@angular/platform-browser';
import { TodoComponent } from './app/todo.component';
import { config } from './app/app.config.server';

const bootstrap = (context: BootstrapContext) =>
    bootstrapApplication(TodoComponent, config, context);

export default bootstrap;
