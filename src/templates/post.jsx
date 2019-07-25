import React from 'react';
import Layout from '../components/layouts';
import ReadLink from '../components/readLink';
import { MDXRenderer } from 'gatsby-plugin-mdx';
import { graphql, Link } from 'gatsby';
import { css } from '@emotion/core';

export const query = graphql`
    query PostTemplate($slug: String!) {
        mdx(frontmatter: { slug: { eq: $slug } }) {
            frontmatter {
                title
                author
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
            <div
                css={css`
                    display: flex;
                    justify-content: space-between;
                    align-items: center;
                `}
            >
                <ReadLink to="/">&larr; Back to Home</ReadLink>
                {previous && <ReadLink to={previous.frontmatter.slug}>Previous</ReadLink>}
                {next && <ReadLink to={next.frontmatter.slug}>Next</ReadLink>}
            </div>
        </Layout>
    );
};

export default PostTemplate;
