import 'npm:@commander-js/extra-typings';
import { program } from 'npm:commander';

program.name('glekcraft')
    .description(
        'A simple Minecraft clone written in Typescript using OpenGL.',
    )
    .version(
        '0.0.0',
        '-v, --version',
        'Display the version information and exit.',
    )
    .helpOption('-h, --help', 'Display the help information and exit.');

program.parseAsync()
    .catch((err: Error) => {
        program.error(err.message, {
            exitCode: 1,
        });
    });

console.log(JSON.stringify(program.opts(), undefined, 4));
