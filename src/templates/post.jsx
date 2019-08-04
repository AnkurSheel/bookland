import { css } from '@emotion/core';
import { graphql } from 'gatsby';
import { MDXRenderer } from 'gatsby-plugin-mdx';
import React from 'react';
import Layout from '../components/layouts';
import ReadLink from '../components/readLink';

const styles = {
    header: css({
        display: 'flex',
    }),
    footerLinks: css({
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
    }),
};
export const query = graphql`
    query PostTemplate($slug: String!) {
        mdx(frontmatter: { slug: { eq: $slug } }) {
            frontmatter {
                title
                author
                date(formatString: "DD MMM YYYY")
                tags
            }
            body
        }
    }
`;

const PostTemplate = ({ data, pageContext }) => {
    const { mdx: post } = data;
    const { next, previous } = pageContext;
    const title = post.frontmatter.title;
    const author = post.frontmatter.author;

    return (
        <Layout>
            <h1>{title}</h1>
            <p> Posted by {author}</p>
            <MDXRenderer>{post.body}</MDXRenderer>
            <div css={styles.footerLinks}>
                <ReadLink to="/">&larr; Back to Home</ReadLink>
                {previous && <ReadLink to={previous.frontmatter.slug}>Previous</ReadLink>}
                {next && <ReadLink to={next.frontmatter.slug}>Next</ReadLink>}
            </div>
        </Layout>
    );
};

export default PostTemplate;
