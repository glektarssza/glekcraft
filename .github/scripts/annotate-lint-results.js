const fsp = require('node:fs/promises');

module.exports = async ({ github, context, glob, result }) => {
    const g = await glob.create('**/format-report.json');
    const paths = await g.glob();
    const fileData = [];
    //-- Read in all data
    for (const p of paths) {
        const d = await fsp.readFile(p, 'utf-8');
        fileData.push(...JSON.parse(d));
    }
    const annotations = [];
    fileData.map((file) => {
        annotations.push(...file.FileChanges.map((change) => {
            let annotation_level;
            if (change.FormatDescription.startsWith('warning')) {
                annotation_level = 'warning';
            } else if (change.FormatDescription.startsWith('error')) {
                annotation_level = 'failure';
            } else if (change.FormatDescription.startsWith('info')) {
                annotation_level = 'notice';
            } else {
                annotation_level = 'notice';
            }
            return {
                start_line: change.LineNumber,
                start_column: change.CharNumber,
                path: file.FilePath,
                annotation_level,
                message: change.FormatDescription
            };
        }));
    });
    await github.rest.checks.create({
        owner: context.repo.owner,
        repo: context.repo.repo,
        name: 'Linting',
        head_sha: context.sha,
        status: 'completed',
        conclusion: result,
        completed_at: new Date().toISOString(),
        output: {
            title: 'Linting Results',
            summary: 'The results of the call to `dotnet format`.',
            annotations
        }
    });
};
