/**
 * Configure your Gatsby site with this file.
 *
 * See: https://www.gatsbyjs.org/docs/gatsby-config/
 */

module.exports = {
    siteMetadata: {
        title: 'Adventures in Bookland',
        description: 'A blog for book reviews',
    },
    /* Your site config here */
    plugins: [
        `gatsby-plugin-typescript`,
        `gatsby-plugin-emotion`,
        `gatsby-plugin-react-helmet`,
        {
            resolve: `gatsby-mdx`,
            options: {
                defaultLayouts: require.resolve('./src/components/layouts.tsx'),
            },
        },
    ],
};
