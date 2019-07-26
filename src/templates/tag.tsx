// Components
import { graphql, Link } from 'gatsby';
import React from 'react';
import GlobalStyles from '../components/globalStyles';
import { MdxConnection, MdxFrontmatter, TagQuery } from '../graphqlTypes';

export const pageQuery = graphql`
    query Tag($tag: String) {
        allMdx(sort: { fields: frontmatter___date, order: ASC }, filter: { frontmatter: { tags: { in: [$tag] } } }) {
            totalCount
            edges {
                node {
                    frontmatter {
                        title
                        slug
                        tags
                    }
                }
            }
        }
    }
`;

interface TagTemplateProps {
    readonly pageContext: {
        readonly tag?: string;
    };
    readonly data: TagQuery;
}

const Tags = ({ pageContext, data }: TagTemplateProps) => {
    console.log({ tag: pageContext.tag, data: data });
    const { tag } = pageContext;
    const { edges, totalCount } = data.allMdx as MdxConnection;
    const tagHeader = `${totalCount} post${totalCount === 1 ? '' : 's'} tagged with "${tag}"`;
    return (
        <>
            <GlobalStyles></GlobalStyles>
            <h1>{tagHeader}</h1>
            <ul>
                {edges.map(({ node }) => {
                    const { title, slug } = node.frontmatter as MdxFrontmatter;
                    return (
                        <li key={slug || ''}>
                            <Link to={slug || ''}>{title}</Link>
                        </li>
                    );
                })}
            </ul>
            <Link to="/tags">All tags</Link>{' '}
        </>
    );
};

export default Tags;
