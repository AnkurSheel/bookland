import { graphql, Link } from 'gatsby';
import React from 'react';
import GlobalStyles from '../components/globalStyles';
import { AllTagsQuery } from '../graphqlTypes';

export const pageQuery = graphql`
    query AllTags {
        allMdx {
            totalCount
            group(field: frontmatter___tags) {
                fieldValue
                totalCount
            }
        }
    }
`;

interface AllTagsProps {
    data: AllTagsQuery;
}

const AllTags = ({ data }: AllTagsProps) => {
    const group = (data.allMdx && data.allMdx.group) || [];
    return (
        <>
            <GlobalStyles></GlobalStyles>
            <h1>Tags</h1>
            <ul>
                {group.map(tag => (
                    <li key={tag.fieldValue || ''}>
                        <Link to={`/tags/${tag.fieldValue}/`}>
                            {tag.fieldValue} ({tag.totalCount})
                        </Link>
                    </li>
                ))}
            </ul>
        </>
    );
};

export default AllTags;
