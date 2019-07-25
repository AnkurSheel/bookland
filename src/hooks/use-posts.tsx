import { graphql, useStaticQuery } from 'gatsby';
import { PostDataQuery } from '../graphqlTypes';
import { IFluidObject } from 'gatsby-background-image';

export interface PostData {
    title: string;
    author: string;
    slug: string;
    image: IFluidObject | undefined;
    excerpt: string;
}

const usePosts = () => {
    const data: PostDataQuery = useStaticQuery(graphql`
        query PostData {
            allMdx(sort: { fields: frontmatter___date, order: DESC }) {
                nodes {
                    frontmatter {
                        author
                        slug
                        title
                        image {
                            sharp: childImageSharp {
                                fluid(
                                    maxWidth: 100
                                    maxHeight: 100
                                    duotone: { shadow: "#663399", highlight: "#ddbbff" }
                                ) {
                                    ...GatsbyImageSharpFluid_withWebp
                                }
                            }
                        }
                    }
                    excerpt
                }
            }
        }
    `) as PostDataQuery;

    return (
        data &&
        data.allMdx &&
        data.allMdx.nodes.map(post => {
            const data: PostData = {
                title: (post.frontmatter && post.frontmatter.title) || '',
                author: (post.frontmatter && post.frontmatter.author) || '',
                slug: (post.frontmatter && post.frontmatter.slug) || '',
                image:
                    (post.frontmatter &&
                        post.frontmatter.image &&
                        post.frontmatter.image.sharp &&
                        (post.frontmatter.image.sharp.fluid as IFluidObject)) ||
                    undefined,
                excerpt: post.excerpt,
            };
            return data;
        })
    );
};

export default usePosts;
