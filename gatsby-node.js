exports.createPages = async ({ actions, graphql, reporter }) => {
    const result = await graphql(`
        query {
            allMdx(sort: { fields: frontmatter___date, order: ASC }) {
                edges {
                    node {
                        frontmatter {
                            slug
                        }
                    }
                }
            }
        }
    `);

    if (result.errors) {
        reporter.panic('Failed to create posts', result.errors);
    }

    const posts = result.data.allMdx.edges;
    posts.forEach(({ node }, index) => {
        const path = node.frontmatter.slug;
        actions.createPage({
            path: path,
            component: require.resolve('./src/templates/post.jsx'),
            context: {
                slug: path,
                previous: index === 0 ? null : posts[index - 1].node,
                next: index === posts.length - 1 ? null : posts[index + 1].node,
            },
        });
    });
};
