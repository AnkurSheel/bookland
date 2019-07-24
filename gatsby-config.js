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
        'gatsby-transformer-sharp',
        'gatsby-plugin-sharp',
        {
            resolve: `gatsby-plugin-mdx`,
            options: {
                defaultLayouts: require.resolve('./src/components/layouts.tsx'),
            },
        },
        {
            resolve: 'gatsby-source-filesystem',
            options: {
                name: 'posts',
                path: './posts',
            },
        },
        {
            resolve: 'gatsby-source-filesystem',
            options: {
                name: 'images',
                path: './images',
            },
        },
    ],
};
