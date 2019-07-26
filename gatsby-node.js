exports.createPages = async ({ actions, graphql, reporter }) => {
    const result = await graphql(`
        query {
            allMdx(sort: { fields: frontmatter___date, order: ASC }) {
                edges {
                    node {
                        frontmatter {
                            slug
                            tags
                        }
                    }
                }
            }
        }
    `);

    if (result.errors) {
        reporter.panic('Failed to create posts', result.errors);
        return;
    }

    const blogPostTemplate = require.resolve('./src/templates/post.jsx');
    const tagTemplate = require.resolve('./src/templates/tag.tsx');

    const posts = result.data.allMdx.edges;
    let allTags = new Set();

    posts.forEach(({ node }, index) => {
        const path = node.frontmatter.slug;
        const tags = node.frontmatter.tags;
        if (!Array.isArray(tags)) {
            return;
        }
        tags.forEach(tag => allTags.add(tag));

        actions.createPage({
            path: path,
            component: blogPostTemplate,
            context: {
                slug: path,
                previous: index === 0 ? null : posts[index - 1].node,
                next: index === posts.length - 1 ? null : posts[index + 1].node,
            },
        });
    });

    allTags.forEach(tag => {
        actions.createPage({
            path: `/tags/${tag}`,
            component: tagTemplate,
            context: {
                tag,
            },
        });
    });
};
